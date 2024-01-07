import { useMutation, useQueryClient } from '@tanstack/react-query';
import { ActionExecutorUpdateDTO, actionExecutorPut, executorKeys } from 'api/actionExecutorApi';
import ActionExecutorEditor from 'components/ActionExecutor/ActionExecutorEditor/ActionExecutorEditor';
import Spinner from 'components/UI/Spinners/Spinner';
import { useParams } from 'react-router-dom';
import MainTemplatePage from './templates/MainTemplatePage';

const ActionExecutorEditPage = () => {
	const { id: idStr } = useParams();
	const id = parseInt(idStr!);

	const queryClient = useQueryClient();
	const saveMutation = useMutation({
		mutationFn: (action: ActionExecutorUpdateDTO) => {
			return actionExecutorPut(action);
		},
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: executorKeys.prefix
			});
		}
	});

	return (
		<MainTemplatePage>
			{saveMutation.isPending && <Spinner />}
			<ActionExecutorEditor onSave={saveMutation.mutate} id={id} />
		</MainTemplatePage>
	);
};

export default ActionExecutorEditPage;
