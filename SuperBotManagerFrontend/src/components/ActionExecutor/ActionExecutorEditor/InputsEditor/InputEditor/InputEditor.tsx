import { FieldInfo } from 'api/actionDefinitionApi';
import { ExecutorInput } from 'api/actionExecutorApi';
import React from 'react';
import { styled } from 'styled-components';
import FieldEditor from './FieldEditor/FieldEditor';

interface InputEditorProps {
	input: ExecutorInput;
	inputSchema: FieldInfo[];
	onChangeInput: (newInput: ExecutorInput) => void;
	invalidInputsNames?: string[];
	onChangeValidation?: (inputName: string, isValid: boolean) => void;
}
const Container = styled.div`
	display: grid;
	grid-template-columns: auto 1fr auto 1fr;
	margin: auto;
	row-gap: 2px;
	@media (max-width: 768px) {
		grid-template-columns: auto 1fr;
	}
`;
const InputEditor: React.FC<InputEditorProps> = ({
	input,
	inputSchema,
	onChangeInput,
	invalidInputsNames,
	onChangeValidation
}) => {
	return (
		<Container>
			{inputSchema.map((fieldInfo) => (
				<>
					<FieldEditor
						key={fieldInfo.name}
						fieldSchema={fieldInfo}
						onChange={(val) => onChangeInput({ ...input, [fieldInfo.name]: val })}
						value={input[fieldInfo.name]}
						isValid={!invalidInputsNames || !invalidInputsNames.includes(fieldInfo.name)}
						onChangeValidation={onChangeValidation}
					/>
				</>
			))}
		</Container>
	);
};

export default InputEditor;
