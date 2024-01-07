import { Select, SelectProps } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import React, { useEffect, useMemo } from 'react';

interface FieldSetEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
	fieldWidthPx?: number;
}

const FieldSetEditor: React.FC<FieldSetEditorProps> = ({
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
		if (value === undefined && fieldSchema.setOptions) {
			onChange(fieldSchema.setOptions[0].value);
		}
	}, [fieldSchema, onChange, value]);
	return (
		<Select
			style={{ width: fieldWidthPx }}
			value={value}
			onChange={(val) => onChange(val)}
			options={options}
		/>
	);
};

export default FieldSetEditor;
