import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { ActionDefinitionDTO } from 'api/actionDefinitionApi';
import {
	ActionExecutorDTO,
	actionExecutorCreate,
	actionExecutorGetAll,
	executorKeys
} from 'api/actionExecutorApi';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { styled } from 'styled-components';
import { useBoolean } from 'usehooks-ts';
import ActionDefinitionPicker from '../ActionDefinitionPicker/ActionDefinitionPicker';
import ActionExecutorItem from './ActionExecutorItem/ActionExecutorItem';
import ActionExecutorItemNew from './ActionExecutorItemNew/ActionExecutorItemNew';

interface ActionsExecutorsListProps {}

const Container = styled.div`
	${ScrollableMixin}
	display: flex;
	flex-flow: row wrap;
	align-content: start;
	gap: 15px;
	/* margin: 15px; */
	/* background-color: ${(a) => a.theme.bgColor}; */
	padding: 30px;
`;

const ActionsExecutorsList: React.FC<ActionsExecutorsListProps> = ({}) => {
	const { data: actionExecutors, isFetching } = useQuery({
		queryKey: executorKeys.list(),
		queryFn: ({ signal }) => actionExecutorGetAll(signal)
	});
	const navigate = useNavigate();
	const queryClient = useQueryClient();
	const addActionExecutorMutation = useMutation({
		mutationFn: (actionDefinition: ActionDefinitionDTO) => {
			return actionExecutorCreate({
				actionDefinitionId: actionDefinition.id,
				actionExecutorName: actionDefinition.actionDefinitionName,
				runPeriod: 'Manual',
				actionData: {
					inputs: []
				},
				preserveExecutedInputs: actionDefinition.preserveExecutedInputs
			});
		},
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: executorKeys.prefix
			});
		}
	});
	const {
		value: isModalAddOpen,
		setTrue: openModalAdd,
		setFalse: closeModalAdd
	} = useBoolean(false);

	const onClickedSettings = (actionExecutor: ActionExecutorDTO) => {
		navigate(`/executor/edit/${actionExecutor.id}`);
	};

	return (
		<>
			{(isFetching || addActionExecutorMutation.isPending) && <Spinner />}
			<Container>
				<ActionDefinitionPicker
					onClose={closeModalAdd}
					isOpen={isModalAddOpen}
					onPick={(action) => {
						closeModalAdd();
						addActionExecutorMutation.mutate(action);
					}}
				/>
				<ActionExecutorItemNew onClick={openModalAdd} />
				{actionExecutors?.map((a) => (
					<ActionExecutorItem key={a.id} actionExecutor={a} onClickedSettings={onClickedSettings} />
				))}
			</Container>
		</>
	);
};

export default ActionsExecutorsList;
