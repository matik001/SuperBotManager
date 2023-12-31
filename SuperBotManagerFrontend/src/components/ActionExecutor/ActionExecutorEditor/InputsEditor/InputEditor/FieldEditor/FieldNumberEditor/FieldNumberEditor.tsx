import { InputNumber } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import React from 'react';

interface FieldNumberEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
	fieldWidthPx?: number;
}

const FieldNumberEditor: React.FC<FieldNumberEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	return (
		<InputNumber
			style={{ width: fieldWidthPx }}
			placeholder="Provide a value"
			value={value}
			onChange={(val) => onChange(val ?? '')}
		></InputNumber>
	);
};

export default FieldNumberEditor;
