import { FieldInfo } from 'api/actionDefinitionApi';
import { ExecutorInput } from 'api/actionExecutorApi';

export const duplicateInput = (input: ExecutorInput, inputSchema: FieldInfo[]) => {
	const inputCopy = JSON.parse(JSON.stringify(input)) as ExecutorInput;
	for (const fieldInfo of inputSchema.filter((a) => a.type === 'Secret')) {
		inputCopy[fieldInfo.name] = undefined;
	}
	return inputCopy;
};

export const MASK_ENCRYPTED = '????????';
