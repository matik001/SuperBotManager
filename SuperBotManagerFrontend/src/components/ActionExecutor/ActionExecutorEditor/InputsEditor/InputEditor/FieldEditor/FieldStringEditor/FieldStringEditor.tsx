import { Input } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import React from 'react';

interface FieldStringEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
}

const FieldStringEditor: React.FC<FieldStringEditorProps> = ({ fieldSchema, onChange, value }) => {
	return (
		<Input
			placeholder="Provide a value"
			value={value}
			onChange={(e) => onChange(e.target.value)}
		></Input>
	);
};

export default FieldStringEditor;
