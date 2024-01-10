import { FieldInfo } from 'api/actionDefinitionApi';
import { ExecutorInput } from 'api/actionExecutorApi';

export const createNewInput = (inputSchema: FieldInfo[]) => {
	const res: ExecutorInput = {};
	for (const field of inputSchema) {
		res[field.name] = {
			isEncrypted: false,
			isValid: true, /// will be changed automaticaly
			value: field.initialValue === undefined ? null : field.initialValue
		};
	}
	return res;
};
export const duplicateInput = (input: ExecutorInput, inputSchema: FieldInfo[]) => {
	const inputCopy = JSON.parse(JSON.stringify(input)) as ExecutorInput;
	// we can duplicate secrets (on edit we create new secret)
	// for (const fieldInfo of inputSchema.filter((a) => a.type === 'Secret')) {
	// 	inputCopy[fieldInfo.name] = undefined;
	// }
	return inputCopy;
};

export const MASK_ENCRYPTED = '????????';
