import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { Button, Divider } from 'antd';
import { actionDefinitionGetAll, definitionKeys } from 'api/actionDefinitionApi';
import { UserDTO } from 'api/userApi';
import { VaultItemUpdateDTO, vaultItemGetAll, vaultItemKeys, vaultItemPut } from 'api/vaultItem';
import FieldEditor from 'components/ActionExecutor/ActionExecutorEditor/InputsEditor/InputEditor/FieldEditor/FieldEditor';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import React, { useEffect, useMemo } from 'react';
import { styled } from 'styled-components';
import { useImmer } from 'use-immer';

interface VaultsProps {
	user: UserDTO;
}

const Container = styled.div`
	padding: 30px;
	margin: 10px;
	${ScrollableMixin}
`;
const Content = styled.div`
	display: flex;
	flex-flow: row wrap;
	gap: 10px;
`;

const GroupIcon = styled.img`
	width: 30px;
	height: 30px;
	border-radius: 5px;
`;
const Group = styled.div`
	width: 224px;
	background-color: ${(p) => p.theme.secondaryBgColor};
	display: flex;
	flex-flow: column nowrap;
	align-items: stretch;
	height: min-content;
	border-radius: 8px;
	overflow: hidden;
`;
const GroupContent = styled.div`
	padding: 12px;
	display: flex;
	flex-flow: column nowrap;
	align-items: center;
	gap: 10px;
	flex-grow: 1;
`;
const GroupTitle = styled.div`
	padding: 12px;
	display: flex;
	justify-content: center;
	align-items: center;
	background-color: ${(p) => p.theme.bgColor3};
	font-size: 22px;
	height: 50px;
	gap: 6px;
`;
const Vaults: React.FC<VaultsProps> = ({ user }) => {
	const { data: vaultItems, isFetching } = useQuery({
		queryKey: vaultItemKeys.list(),
		queryFn: ({ signal }) => vaultItemGetAll(signal)
	});
	const { data: definitions, isFetching: isFetchingDefinitions } = useQuery({
		queryKey: definitionKeys.list(),
		queryFn: ({ signal }) => actionDefinitionGetAll(signal)
	});
	const [localVaultItems, updateLocalVaultItems] = useImmer<VaultItemUpdateDTO[]>([]);
	useEffect(() => {
		if (vaultItems)
			updateLocalVaultItems(
				vaultItems.map((a) => ({
					...a,
					plainValue: null
				}))
			);
	}, [updateLocalVaultItems, vaultItems]);
	const vaults = useMemo(() => {
		const res = localVaultItems?.reduce<Record<string, VaultItemUpdateDTO[]>>((agr, x) => {
			if (!agr[x.vaultGroupName]) agr[x.vaultGroupName] = [];
			agr[x.vaultGroupName].push(x);
			return agr;
		}, {});
		return res;
	}, [localVaultItems]);
	const onCancel = (groupName: string) => {
		updateLocalVaultItems((items) => {
			for (const item of items) {
				if (item.vaultGroupName === groupName) {
					item.plainValue = null;
				}
			}
		});
	};
	const queryClient = useQueryClient();
	const updateGroupMutation = useMutation({
		mutationFn: async (groupName: string) => {
			for (const vaultItem of localVaultItems) {
				if (vaultItem.vaultGroupName === groupName) {
					await vaultItemPut(vaultItem);
				}
			}
		},
		onSettled: async () => {
			queryClient.invalidateQueries({ queryKey: vaultItemKeys.prefix });
		}
	});
	return (
		<Container>
			{/* <div style={{ fontSize: '32px', fontWeight: 300 }}>{`${user.userName}'s`} Vaults</div> */}
			<Content>
				{(isFetching || isFetchingDefinitions) && <Spinner />}
				{vaults &&
					Object.keys(vaults)
						.sort()
						.map((groupName) => {
							const vaultItems = vaults[groupName];
							const groupDefinitions = definitions?.filter(
								(a) => a.actionDefinitionGroup === groupName
							);
							const groupFields =
								groupDefinitions?.flatMap((a) => a.actionDataSchema.inputSchema) ?? [];
							const iconSrc =
								(groupDefinitions &&
									groupDefinitions.length &&
									groupDefinitions[0].actionDefinitionIcon) ||
								'';
							const isGroupChanged = vaultItems.some((f) => f.plainValue !== null);
							return (
								<Group key={groupName}>
									<GroupTitle>
										<GroupIcon src={iconSrc}></GroupIcon>
										{groupName}
									</GroupTitle>
									<GroupContent>
										{updateGroupMutation.variables === groupName &&
											updateGroupMutation.isPending && <Spinner />}
										{vaultItems.map((vaultItem) => {
											const field = groupFields.find((a) => a.name === vaultItem.fieldName);
											if (!field) return <></>;
											return (
												<div
													key={vaultItem.id}
													// style={{
													// 	display: 'flex',
													// 	flexFlow: 'column wrap',
													// 	gap: '2px',
													// 	alignItems: 'start'
													// }}
												>
													<FieldEditor
														fieldSchema={{
															...field,
															isOptional: true
														}}
														labelStyle={{
															marginBottom: '3px'
														}}
														onChange={(val) => {
															updateLocalVaultItems((items) => {
																const item = items.find((a) => a.id === vaultItem.id);
																if (item) {
																	item.plainValue = val?.value ?? null;
																}
															});
														}}
														value={{
															isEncrypted: vaultItem.plainValue === null && !!vaultItem.secretId,
															isValid: true,
															value: vaultItem.plainValue ?? vaultItem.secretId?.toString() ?? null
														}}
														fieldWidthPx={200}
													/>
												</div>
											);
										})}
										<Divider style={{ margin: '15px 0 4px 0' }} />
										<div
											style={{
												width: '100%',
												display: 'flex',
												justifyContent: 'center',
												gap: '10px',
												marginBottom: '4px'
											}}
										>
											<Button
												disabled={!isGroupChanged}
												type="primary"
												onClick={() => updateGroupMutation.mutate(groupName)}
											>
												Save
											</Button>
											<Button
												disabled={!isGroupChanged}
												onClick={() => onCancel(groupName)}
												type="primary"
												danger
											>
												Cancel
											</Button>
										</div>
									</GroupContent>
								</Group>
							);
						})}
			</Content>
		</Container>
	);
};

export default Vaults;
