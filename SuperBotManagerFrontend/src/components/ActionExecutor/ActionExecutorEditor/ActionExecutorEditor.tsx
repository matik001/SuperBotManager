import { useQuery } from '@tanstack/react-query';
import {
	ActionExecutorExtendedDTO,
	ActionExecutorUpdateDTO,
	actionExecutorGetOne,
	executorKeys
} from 'api/actionExecutorApi';
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
	max-width: 1100px;
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
	return (
		<Container>
			<TopHeader
				canSave={canSave}
				onGoBack={onGoBack}
				hasUnsaveChanges={hasUnsaveChanges}
				title={executorLocal?.actionExecutorName ?? ''}
				onSave={() => executorLocal && onSave(executorLocal)}
				iconUrl={executorLocal?.actionDefinition?.actionDefinitionIcon}
			/>
			<Content>
				{!isFetching && executorLocal ? (
					<>
						<ExecutorSettings executor={executorLocal} updateExecutor={updateExecutorLocal} />
						<InputsEditor
							style={{ marginTop: '10px' }}
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
