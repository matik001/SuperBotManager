import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import {
	ActionExecutorExtendedDTO,
	ActionExecutorUpdateDTO,
	actionExecutorDelete,
	actionExecutorGetOne,
	executorKeys
} from 'api/actionExecutorApi';
import { vaultItemGetAll, vaultItemKeys } from 'api/vaultItem';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import deepEqual from 'deep-equal';
import React, { useEffect, useMemo } from 'react';
import { useNavigate } from 'react-router-dom';
import { styled } from 'styled-components';
import { useImmer } from 'use-immer';
import ExecutorSettings from './ExecutorSettings/ExecutorSettings';
import InputsEditor from './InputsEditor/InputsEditor';
import TopHeader from './TopHeader/TopHeader';

interface ActionExecutorEditorProps {
	id: number;
	onSave: (action: ActionExecutorUpdateDTO) => void;
}
const Container = styled.div`
	margin: 15px;
	border-radius: 15px;
	overflow: hidden;
	max-width: 1130px;
	margin-left: auto;
	margin-right: auto;
	height: calc(100% - 15px);
	padding-bottom: 40px;
`;
const Content = styled.div`
	${ScrollableMixin}
	min-height: 500px;
	background-color: ${(t) => (t.theme.isDarkMode ? t.theme.bgColor3 : t.theme.secondaryBgColor)};
	position: relative;
	padding: 30px;
	display: flex;
	flex-direction: column;
	gap: 5px;
`;

const ActionExecutorEditor: React.FC<ActionExecutorEditorProps> = ({ id, onSave }) => {
	const { data: actionExecutor, isFetching } = useQuery({
		queryKey: executorKeys.one(id),
		queryFn: ({ signal }) => actionExecutorGetOne(id, signal)
	});
	const { data: vaultItems, isFetching: isFetchingVault } = useQuery({
		queryKey: vaultItemKeys.list(),
		queryFn: ({ signal }) => vaultItemGetAll(signal)
	});
	const executorVaultItems = useMemo(
		() =>
			vaultItems?.filter(
				(a) => a.vaultGroupName === actionExecutor?.actionDefinition?.actionDefinitionGroup
			),
		[actionExecutor?.actionDefinition?.actionDefinitionGroup, vaultItems]
	);
	const [executorLocal, updateExecutorLocal] = useImmer<ActionExecutorExtendedDTO | undefined>(
		undefined
	);
	useEffect(() => {
		updateExecutorLocal(actionExecutor);
	}, [actionExecutor, updateExecutorLocal]);

	const hasUnsaveChanges = useMemo(
		() => !deepEqual(executorLocal, actionExecutor),
		[actionExecutor, executorLocal]
	);
	const canSave = hasUnsaveChanges && executorLocal !== undefined && !isFetching;
	const navigate = useNavigate();
	const onGoBack = () => {
		navigate('/executors');
	};
	const queryClient = useQueryClient();
	const deleteExecutorMutation = useMutation({
		mutationFn: () => actionExecutorDelete(id),
		onSuccess: async () => {
			await queryClient.invalidateQueries();
			onGoBack();
		}
	});
	return (
		<Container>
			<TopHeader
				isDeleting={deleteExecutorMutation.isPending}
				canSave={canSave}
				onGoBack={onGoBack}
				hasUnsaveChanges={hasUnsaveChanges}
				title={executorLocal?.actionExecutorName ?? ''}
				onSave={() => executorLocal && onSave(executorLocal)}
				iconUrl={executorLocal?.actionDefinition?.actionDefinitionIcon}
				onDelete={deleteExecutorMutation.mutate}
			/>
			<Content>
				{!isFetching && executorLocal && !deleteExecutorMutation.isPending && !isFetchingVault ? (
					<>
						<ExecutorSettings executor={executorLocal} updateExecutor={updateExecutorLocal} />
						<InputsEditor
							style={{ marginTop: '10px' }}
							inputs={executorLocal.actionData.inputs}
							inputSchema={executorLocal.actionDefinition.actionDataSchema.inputSchema}
							executorVaultItems={executorVaultItems}
							onChangeInputs={(newInputs) =>
								updateExecutorLocal((action) => {
									action!.actionData.inputs = newInputs;
								})
							}
						/>
					</>
				) : (
					<Spinner />
				)}
			</Content>
		</Container>
	);
};

export default ActionExecutorEditor;
