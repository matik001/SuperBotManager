import { ActionExecutorDTO } from 'api/actionExecutorApi';
import React from 'react';

interface ActionExecutorItemProps {
	actionExecutor: ActionExecutorDTO;
}

const ActionExecutorItem: React.FC<ActionExecutorItemProps> = ({ actionExecutor }) => {
	return (
		<>
			{actionExecutor.actionExecutorName} ({actionExecutor.id})
		</>
	);
};

export default ActionExecutorItem;
