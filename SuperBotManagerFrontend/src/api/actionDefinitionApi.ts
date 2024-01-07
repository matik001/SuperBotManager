import { appAxios } from './apiConfig';

export type FieldType =
	| 'String'
	| 'Number'
	| 'Secret'
	| 'Date'
	| 'DateTime'
	| 'Boolean'
	| 'Json'
	| 'Set'
	| 'ExecutorPicker';
export interface SetOption {
	display: string;
	value: string;
}
export interface FieldInfo {
	name: string;
	description: string;
	isOptional: boolean;
	type: FieldType;
	setOptions?: SetOption[];
}
export interface ActionDefinitionSchema {
	inputSchema: FieldInfo[];
	outputSchema: FieldInfo[];
}
export interface ActionDefinitionDTO {
	id: number;
	actionDefinitionName: string;
	actionDefinitionDescription: string;
	actionDefinitionIcon: string;
	preserveExecutedInputs: boolean;

	actionDataSchema: ActionDefinitionSchema;
	createdDate: Date;
	modifiedDate: Date;
}

export const definitionKeys = {
	prefix: ['definition'] as const,
	list: () => [...definitionKeys.prefix, 'list'] as const,
	one: (id: number) => [...definitionKeys.prefix, 'one', id] as const
};
export const actionDefinitionGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<ActionDefinitionDTO[]>('/v1/ActionDefinition', {
		signal: signal
	});
	return res.data;
};

export const actionDefinitionGetOne = async (id: number, signal: AbortSignal) => {
	const res = await appAxios.get<ActionDefinitionDTO>(`/v1/ActionDefinition/${id}`, {
		signal: signal
	});
	return res.data;
};
