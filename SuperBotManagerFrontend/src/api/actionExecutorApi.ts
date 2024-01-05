import { appAxios } from './apiConfig';
export type RunPeriod = 'Manual' | 'Everyday' | 'Loop' | 'TimePeriod';

export type ExecutorInput = Record<string, string>;
export interface ActionExecutorSchema {
	Inputs: ExecutorInput[];
}
export interface ActionExecutorCreateDTO {
	actionExecutorName: string;
	actionData: ActionExecutorSchema;
	actionDefinitionId: number;
	runPeriod: RunPeriod;
	timeIntervalSeconds?: number;
	actionExecutorOnFinishId?: number;
}
export interface ActionExecutorDTO extends ActionExecutorCreateDTO {
	id: number;
	lastRunDate?: Date;
	createdDate: Date;
	modifiedDate: Date;
}

export const QUERYKEY_ACTIONEXECUTOR_GETALL = 'QUERYKEY_ACTIONEXECUTOR_GETALL';
export const actionExecutorGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<ActionExecutorDTO[]>('/v1/ActionExecutor', {
		signal: signal
	});
	return res.data;
};

export const QUERYKEY_ACTIONEXECUTOR_CREATE = 'QUERYKEY_ACTIONEXECUTOR_CREATE';
export const actionExecutorCreate = async (
	actionExecutorDTO: ActionExecutorCreateDTO,
	signal?: AbortSignal
) => {
	const res = await appAxios.post<undefined>('/v1/ActionExecutor', actionExecutorDTO, {
		signal: signal
	});
	return res.data;
};
