import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { ActionDefinitionDTO } from 'api/actionDefinitionApi';
import {
	QUERYKEY_ACTIONEXECUTOR_GETALL,
	actionExecutorCreate,
	actionExecutorGetAll
} from 'api/actionExecutorApi';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import React from 'react';
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
`;

const ActionsExecutorsList: React.FC<ActionsExecutorsListProps> = ({}) => {
	const { data: actionExecutors, isFetching } = useQuery({
		queryKey: [QUERYKEY_ACTIONEXECUTOR_GETALL],
		queryFn: ({ signal }) => actionExecutorGetAll(signal)
	});
	const queryClient = useQueryClient();
	const addActionExecutorMutation = useMutation({
		mutationFn: (actionDefinition: ActionDefinitionDTO) => {
			return actionExecutorCreate({
				actionDefinitionId: actionDefinition.id,
				actionExecutorName: actionDefinition.actionDefinitionName,
				runPeriod: 'Manual',
				actionData: {
					Inputs: []
				}
			});
		},
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: [QUERYKEY_ACTIONEXECUTOR_GETALL]
			});
		}
	});
	const { value: isEditing, setTrue: edit, setFalse: closeEdit } = useBoolean(false);
	return (
		<>
			{isFetching || (addActionExecutorMutation.isPending && <Spinner />)}
			<Container>
				<ActionDefinitionPicker
					onClose={closeEdit}
					isOpen={isEditing}
					onPick={(action) => {
						closeEdit();
						addActionExecutorMutation.mutate(action);
					}}
				/>
				<ActionExecutorItemNew onClick={edit} />
				{actionExecutors?.map((a) => <ActionExecutorItem key={a.id} actionExecutor={a} />)}
			</Container>
		</>
	);
};

export default ActionsExecutorsList;
