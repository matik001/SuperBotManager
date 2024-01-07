import { InputNumber } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import React from 'react';

interface FieldNumberEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
}

const FieldNumberEditor: React.FC<FieldNumberEditorProps> = ({ fieldSchema, onChange, value }) => {
	return (
		<InputNumber
			style={{ width: '200px' }}
			placeholder="Provide a value"
			value={value}
			onChange={(val) => onChange(val ?? '')}
		></InputNumber>
	);
};

export default FieldNumberEditor;
