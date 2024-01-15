import { ActionExecutorDTO } from './actionExecutorApi';
import { appAxios } from './apiConfig';
export type RunMethod = 'Manual' | 'Automatic';

export const scheduleKeys = {
	prefix: ['schedule'] as const,
	list: () => [...scheduleKeys.prefix, 'list'] as const,
	one: (id: number) => [...scheduleKeys.prefix, 'one', id] as const
};

export interface ActionScheduleCreateDTO {
	enabled: boolean;
	actionScheduleName: string;
	executorId: number;
	nextRun: Date;
	type: 'Period' | 'Once';
	intervalSec: number;
}

export interface ActionScheduleUpdateDTO extends ActionScheduleCreateDTO {
	id: number;
}

export interface ActionScheduleDTO extends ActionScheduleUpdateDTO {
	executor: ActionExecutorDTO;
	createdDate: Date;
	modifiedDate: Date;
}
export const scheduleGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<ActionScheduleDTO[]>('/v1/Schedule', {
		signal: signal
	});
	return res.data;
};

export const scheduleCreate = async (
	actionExecutorDTO: ActionScheduleCreateDTO,
	signal?: AbortSignal
) => {
	const res = await appAxios.post<undefined>('/v1/Schedule', actionExecutorDTO, {
		signal: signal
	});
	return res.data;
};

export const scheduleGetOne = async (id: number, signal: AbortSignal) => {
	const res = await appAxios.get<ActionScheduleDTO>(`/v1/Schedule/${id}`, {
		signal: signal
	});
	return res.data;
};

export const schedulePut = async (schedule: ActionScheduleUpdateDTO, signal?: AbortSignal) => {
	const res = await appAxios.put(`/v1/Schedule/${schedule.id}`, schedule, {
		signal: signal
	});
	return res.data;
};

export const scheduleDelete = async (id: number, signal?: AbortSignal) => {
	const res = await appAxios.delete(`/v1/Schedule/${id}`, {
		signal: signal
	});
	return res.data;
};
