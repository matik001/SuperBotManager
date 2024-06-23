import { appAxios } from './apiConfig';
export type RunMethod = 'Manual' | 'Automatic';

export interface ActionSchema {
	input: Record<string, string>;
	output: Record<string, string>;
}
export type ActionStatus = 'Pending' | 'InProgress' | 'Finished' | 'Error' | 'Canceled';
export interface ActionUpdateDTO {
	id: number;
	actionData: ActionSchema;
	actionStatus: ActionStatus;
}
export interface ActionDTO extends ActionUpdateDTO {
	actionExecutorId: number;

	runStartType: 'Manual' | 'Scheduled';
	forwardedFromActionId?: number;

	createdDate: Date;
	modifiedDate: Date;
}

export const actionKeys = {
	prefix: ['action'] as const,
	list: () => [...actionKeys.prefix, 'list'] as const,
	one: (id: number) => [...actionKeys.prefix, 'one', id] as const
};
export const actionGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<ActionDTO[]>('/v1/Action', {
		signal: signal
	});
	return res.data;
};
export const actionGetOne = async (id: number, signal: AbortSignal) => {
	const res = await appAxios.get<ActionDTO>(`/v1/Action/${id}`, {
		signal: signal
	});
	return res.data;
};

export const actionPut = async (action: ActionUpdateDTO, signal?: AbortSignal) => {
	const res = await appAxios.put(`/v1/Action/${action.id}`, action, {
		signal: signal
	});
	return res.data;
};

// export const actionDelete = async (id: number, signal?: AbortSignal) => {
// 	const res = await appAxios.delete(`/v1/ActionExecutor/${id}`, {
// 		signal: signal
// 	});
// 	return res.data;
// };
