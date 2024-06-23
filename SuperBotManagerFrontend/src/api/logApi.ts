import useUserStore from 'store/userStore';
import { appAxios } from './apiConfig';
import { UserDTO } from './userApi';

export type LogType = 'Info' | 'Error' | 'Warning' | 'Debug';
export const LOG_APP = 'SuperBotFrontned';
export const LOG_MODULE = '';

export const createLog = (type: LogType, title: string, description: string, module?: string) => {
	return {
		logApp: LOG_APP,
		logModule: module ?? LOG_MODULE,
		logTitle: title,
		logDetails: description,
		userId: useUserStore.getState().user?.id,
		logType: type
	} as LogCreateDTO;
};

export interface LogCreateDTO {
	logApp: string;
	logTitle: string;
	logDetails?: string;
	logType: LogType;
	logModule: string;
	userId: number | undefined;
	user: UserDTO | undefined;
}

export interface LogDTO extends LogCreateDTO {
	id: number;
	createdDate: Date;
	modifiedDate: Date;
}

export const logKeys = {
	prefix: ['log'] as const,
	list: () => [...logKeys.prefix, 'list'] as const,
	one: (id: number) => [...logKeys.prefix, 'one', id] as const
};
export const logCreate = async (log: LogCreateDTO, signal?: AbortSignal) => {
	const res = await appAxios.post(`/v1/Log`, log, {
		signal: signal
	});
	return res.data;
};

export const logGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<LogDTO[]>('/v1/Log', {
		signal: signal
	});
	return res.data;
};

export const logGetOne = async (id: number, signal: AbortSignal) => {
	const res = await appAxios.get<LogDTO>(`/v1/Log/${id}`, {
		signal: signal
	});
	return res.data;
};

export const logPost = async (log: LogCreateDTO, signal?: AbortSignal) => {
	const res = await appAxios.post(`/v1/Log`, log, {
		signal: signal
	});
	return res.data;
};
