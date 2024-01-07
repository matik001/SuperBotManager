import { useQuery } from '@tanstack/react-query';
import { Button, Popconfirm } from 'antd';
import {
	ActionExecutorExtendedDTO,
	ActionExecutorUpdateDTO,
	actionExecutorGetOne,
	executorKeys
} from 'api/actionExecutorApi';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import dayjs from 'dayjs';
import deepEqual from 'deep-equal';
import React, { useEffect, useMemo, useState } from 'react';
import { IoArrowBack } from 'react-icons/io5';
import { useNavigate } from 'react-router-dom';
import { styled } from 'styled-components';
import { useImmer } from 'use-immer';
import InputsEditor from './InputsEditor/InputsEditor';

interface ActionExecutorEditorProps {
	id: number;
	onSave: (action: ActionExecutorUpdateDTO) => void;
}
const Container = styled.div`
	margin: 15px;
	border-radius: 15px;
	overflow: hidden;
	max-width: 1100px;
	margin-left: auto;
	margin-right: auto;
	height: calc(100% - 15px);
	padding-bottom: 40px;
`;
const Header = styled.div`
	background-color: ${(t) => t.theme.secondaryBgColor};
	padding: 5px 20px 5px 10px;
	display: flex;
	flex-flow: row nowrap;
	align-items: center;
	justify-content: space-between;
`;
const Content = styled.div`
	${ScrollableMixin}
	min-height: 500px;
	background-color: ${(t) => t.theme.bgColor3};
	position: relative;
	padding: 30px;
	display: flex;
	flex-direction: column;
	gap: 5px;
`;
const Row = styled.div`
	display: flex;
	flex-direction: row;
	align-items: center;
	gap: 5px;
`;
const Column = styled.div`
	display: flex;
	flex-direction: column;
	gap: 5px;
`;
const ContentItem = styled.div``;
const ActionExecutorEditor: React.FC<ActionExecutorEditorProps> = ({ id, onSave }) => {
	const { data: actionExecutor, isFetching } = useQuery({
		queryKey: executorKeys.one(id),
		queryFn: ({ signal }) => actionExecutorGetOne(id, signal)
	});
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
	const canSave = hasUnsaveChanges;
	const navigate = useNavigate();
	const [isUnsavedMsgPopupOpen, setUnsavedMsgPopupOpen] = useState(false);
	const onGoBack = () => {
		navigate('/executors');
	};
	return (
		<Container>
			<Header>
				<Popconfirm
					title="Are you sure?"
					description="You have unsaved changes!"
					okText="Yes"
					cancelText="No"
					onConfirm={onGoBack}
					onCancel={() => {}}
					open={isUnsavedMsgPopupOpen}
					onOpenChange={(open) => {
						if (open && !hasUnsaveChanges) onGoBack();
						setUnsavedMsgPopupOpen(open);
					}}
					// onPopupClick={(e) => (!hasUnsaveChanges && onGoBack()) || setUnsavedMsgPopupOpen(true)}
				>
					<Button
						icon={<IoArrowBack />}
						ghost
						shape="circle"
						type="text"
						style={{ width: '42px', height: '42px', fontSize: '22px' }}
						onClick={(e) => e.stopPropagation()}
					/>
				</Popconfirm>

				<div style={{ display: 'flex', flexDirection: 'row', alignItems: 'center', gap: '6px' }}>
					{actionExecutor && (
						<img
							style={{ width: '28px', height: '28px', borderRadius: '6px' }}
							src={actionExecutor.actionDefinition.actionDefinitionIcon}
						></img>
					)}
					{actionExecutor?.actionExecutorName}
				</div>
				<Button
					disabled={!canSave || !executorLocal}
					type="primary"
					onClick={() => executorLocal && onSave(executorLocal)}
				>
					Save
				</Button>
			</Header>
			<Content>
				{!isFetching && executorLocal ? (
					<>
						<Row style={{ gap: '20px' }}>
							<img
								style={{ width: '190px', height: '190px', borderRadius: '12px' }}
								src={executorLocal.actionDefinition.actionDefinitionIcon}
							></img>
							<Column>
								<ContentItem style={{ fontSize: '28px', fontWeight: '100', marginBottom: '5px' }}>
									{executorLocal.actionDefinition.actionDefinitionName}
								</ContentItem>
								<ContentItem>
									ID: <b>{executorLocal.id}</b>
								</ContentItem>
								<ContentItem>
									Created: <b>{dayjs(executorLocal.createdDate).format('L LTS')}</b>
								</ContentItem>
								<ContentItem>
									Modified: <b>{dayjs(executorLocal.modifiedDate).format('L LTS')}</b>
								</ContentItem>
								<ContentItem>
									In queue: <b>0</b>
								</ContentItem>
								<ContentItem>
									Last run:{' '}
									<b>
										{executorLocal.lastRunDate ? dayjs(executorLocal.lastRunDate).toNow() : 'never'}
									</b>
								</ContentItem>
							</Column>
						</Row>
						<InputsEditor
							inputs={executorLocal.actionData.inputs}
							inputSchema={executorLocal.actionDefinition.actionDataSchema.inputSchema}
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
