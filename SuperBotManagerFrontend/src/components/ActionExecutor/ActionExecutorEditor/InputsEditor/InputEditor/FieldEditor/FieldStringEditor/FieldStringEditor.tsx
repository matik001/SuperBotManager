import { Input } from 'antd';
import React from 'react';
import { InnerFieldEditorProps } from '../FieldEditor';

const FieldStringEditor: React.FC<InnerFieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	return (
		<Input
			style={{ width: fieldWidthPx }}
			placeholder="Provide a value"
			value={value.value ?? undefined}
			onChange={(e) => onChange({ ...value, value: e.target.value })}
		></Input>
	);
};

export default FieldStringEditor;
