import { useQuery } from '@tanstack/react-query';
import { actionGetAll, actionKeys } from 'api/actionApi';
import { actionExecutorGetAll, executorKeys } from 'api/actionExecutorApi';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import React from 'react';
import { styled } from 'styled-components';
import QueueItem from './QueueItem/QueueItem';

interface ActionsQueueProps {}

const Container = styled.div`
	padding: 20px;
	margin: 10px;
	${ScrollableMixin}
`;

const ActionsQueue: React.FC<ActionsQueueProps> = ({}) => {
	const {
		data: actions,
		isFetching: isFetchingActions,
		isLoading
	} = useQuery({
		queryKey: actionKeys.list(),
		queryFn: ({ signal }) => actionGetAll(signal),
		refetchOnMount: 'always',
		refetchInterval: 3000 /// if more users it can be decreased or we can use signalr
	});
	const { data: executors, isFetching: isFetchingExecutors } = useQuery({
		queryKey: executorKeys.list(),
		queryFn: ({ signal }) => actionExecutorGetAll(signal)
	});
	return (
		<Container>
			{(isLoading || isFetchingExecutors) && <Spinner />}

			{executors &&
				actions?.map((action) => {
					const executor = executors?.find((a) => a.id === action.actionExecutorId);
					if (!executor) return <></>;
					return <QueueItem key={action.id} action={action} executor={executor} />;
				})}
		</Container>
	);
};

export default ActionsQueue;
