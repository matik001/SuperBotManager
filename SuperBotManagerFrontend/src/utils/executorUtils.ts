import { FieldInfo, FieldType } from 'api/actionDefinitionApi';
import { ExecutorInput } from 'api/actionExecutorApi';
import { VaultItemDTO } from 'api/vaultItem';

const initialValues: Record<FieldType, string | null> = {
	Boolean: 'false',
	Date: null,
	DateTime: null,
	ExecutorPicker: null,
	Json: '{}',
	Number: '0',
	Secret: null,
	Set: null,
	String: ''
};
export const createNewInput = (
	inputSchema: FieldInfo[],
	vaultItems: VaultItemDTO[] | undefined
) => {
	const res: ExecutorInput = {};
	for (const field of inputSchema) {
		if (field.type === 'Secret') {
			const vaultItem = vaultItems?.find((a) => a.fieldName === field.name);
			if (vaultItem?.secretId) {
				res[field.name] = {
					isEncrypted: true,
					isValid: true,
					value: vaultItem.secretId.toString()
				};
				continue;
			}
		}
		res[field.name] = {
			isEncrypted: false,
			isValid: true, /// will be changed automaticaly
			value: field.initialValue === undefined ? null : field.initialValue
		};
		if (res[field.name]!.value === null) {
			res[field.name]!.value = initialValues[field.type];
		}
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
