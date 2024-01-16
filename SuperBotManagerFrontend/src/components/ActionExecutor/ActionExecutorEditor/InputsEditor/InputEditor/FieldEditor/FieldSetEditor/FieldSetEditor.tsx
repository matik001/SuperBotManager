import { Select, SelectProps } from 'antd';
import React, { useEffect, useMemo } from 'react';
import { InnerFieldEditorProps } from '../FieldEditor';

const FieldSetEditor: React.FC<InnerFieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	const options = useMemo<SelectProps['options']>(
		() =>
			fieldSchema.setOptions!.map((option) => ({
				value: option.value,
				label: option.display
			})) as SelectProps['options'],
		[fieldSchema.setOptions]
	);
	useEffect(() => {
		if (value.value === undefined && fieldSchema.setOptions) {
			onChange({ ...value, value: fieldSchema.setOptions[0].value });
		}
	}, [fieldSchema, onChange, value]);
	return (
		<Select
			disabled={value.disabled}
			style={{ width: fieldWidthPx }}
			value={value.value}
			onChange={(val) => onChange({ ...value, value: val })}
			options={options}
		/>
	);
};

export default FieldSetEditor;
