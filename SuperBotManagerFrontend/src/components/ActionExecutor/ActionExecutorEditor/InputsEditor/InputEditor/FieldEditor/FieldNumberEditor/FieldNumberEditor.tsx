import { InputNumber } from 'antd';
import React from 'react';
import { InnerFieldEditorProps } from '../FieldEditor';

const FieldNumberEditor: React.FC<InnerFieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	return (
		<InputNumber
			style={{ width: fieldWidthPx }}
			placeholder={fieldSchema.placeholder ?? 'Provide a value'}
			value={value.value}
			onChange={(val) => onChange({ ...value, value: val ?? '' })}
		></InputNumber>
	);
};

export default FieldNumberEditor;
