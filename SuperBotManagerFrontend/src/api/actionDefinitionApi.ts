import { appAxios } from './apiConfig';

type FieldType = 'String' | 'Number' | 'DateTime';
export interface SetOption {
	display: string;
	value: string;
}
export interface FieldInfo {
	name: string;
	description: string;
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
	actionDataSchema: ActionDefinitionSchema;
	createdDate: Date;
	modifiedDate: Date;
}

export const QUERYKEY_ACTIONDEFINITION_GETALL = 'QUERYKEY_ACTIONDEFINITION_GETALL';
export const actionDefinitionGetAll = async (signal: AbortSignal) => {
	const res = await appAxios.get<ActionDefinitionDTO[]>('/v1/ActionDefinition', {
		signal: signal
	});
	return res.data;
};
