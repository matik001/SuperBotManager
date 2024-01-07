import { Input } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import React from 'react';

interface FieldSecretEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
	fieldWidthPx?: number;
}

const FieldSecretEditor: React.FC<FieldSecretEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	return (
		<Input.Password
			style={{ width: fieldWidthPx }}
			placeholder="Provide a value"
			value={value}
			onChange={(e) => onChange(e.target.value)}
		></Input.Password>
	);
};

export default FieldSecretEditor;
