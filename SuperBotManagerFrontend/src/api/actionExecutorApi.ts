import { ActionDefinitionDTO } from './actionDefinitionApi';
import { appAxios } from './apiConfig';
export type RunPeriod = 'Manual' | 'Everyday' | 'Loop' | 'TimePeriod';

export interface FieldValue {
	value: string | undefined;
	isEncrypted: boolean;
	isValid: boolean;
}
export type ExecutorInput = Record<string, FieldValue | undefined>;
export interface ActionExecutorSchema {
	inputs: ExecutorInput[];
}
export interface ActionExecutorCreateDTO {
	actionExecutorName: string;
	actionData: ActionExecutorSchema;
	actionDefinitionId: number;
	runPeriod: RunPeriod;
	preserveExecutedInputs: boolean;
	timeIntervalSeconds?: number;
	actionExecutorOnFinishId?: number;
}
export interface ActionExecutorUpdateDTO extends ActionExecutorCreateDTO {
	id: number;
}
export interface ActionExecutorDTO extends ActionExecutorUpdateDTO {
	isValid: boolean;
	lastRunDate?: Date;
	createdDate: Date;
	modifiedDate: Date;
}
export interface ActionExecutorExtendedDTO extends ActionExecutorDTO {
	actionDefinition: ActionDefinitionDTO;
}

export const executorKeys = {
	prefix: ['executor'] as const,
	list: () => [...executorKeys.prefix, 'list'] as const,
	one: (id: number) => [...executorKeys.prefix, 'one', id] as const
};
export const actionExecutorGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<ActionExecutorDTO[]>('/v1/ActionExecutor', {
		signal: signal
	});
	return res.data;
};

export const actionExecutorCreate = async (
	actionExecutorDTO: ActionExecutorCreateDTO,
	signal?: AbortSignal
) => {
	const res = await appAxios.post<undefined>('/v1/ActionExecutor', actionExecutorDTO, {
		signal: signal
	});
	return res.data;
};

export const actionExecutorGetOne = async (id: number, signal: AbortSignal) => {
	const res = await appAxios.get<ActionExecutorExtendedDTO>(`/v1/ActionExecutor/${id}`, {
		signal: signal
	});
	return res.data;
};

export const actionExecutorPut = async (action: ActionExecutorUpdateDTO, signal?: AbortSignal) => {
	const res = await appAxios.put(`/v1/ActionExecutor/${action.id}`, action, {
		signal: signal
	});
	return res.data;
};

export const actionExecutorDelete = async (id: number, signal?: AbortSignal) => {
	const res = await appAxios.delete(`/v1/ActionExecutor/${id}`, {
		signal: signal
	});
	return res.data;
};

export const actionExecutorRun = async (id: number, signal?: AbortSignal) => {
	const res = await appAxios.post<undefined>(
		`/v1/ActionExecutor/${id}/execute`,
		{},
		{
			signal: signal
		}
	);
	return res.data;
};
