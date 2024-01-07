import { useQuery } from '@tanstack/react-query';
import { Select, SelectProps } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import { actionExecutorGetAll, executorKeys } from 'api/actionExecutorApi';
import React, { useEffect, useMemo } from 'react';

interface FieldExecutorPickerEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
	fieldWidthPx?: number;
}

const FieldExecutorPickerEditor: React.FC<FieldExecutorPickerEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	const { data: executors, isFetching: isFetchingExecutors } = useQuery({
		queryKey: executorKeys.list(),
		queryFn: ({ signal }) => actionExecutorGetAll(signal)
	});

	const options = useMemo<SelectProps['options']>(() => {
		if (!executors) return [];
		let newOptions = (
			executors
				? executors.map((executor) => ({
						value: executor.id.toString(),
						label: executor.actionExecutorName
					}))
				: []
		) as SelectProps['options'];
		if (fieldSchema.isOptional) {
			newOptions = [
				{
					label: 'None',
					value: ''
				},
				...newOptions!
			];
		}
		return newOptions;
	}, [executors, fieldSchema.isOptional]);

	useEffect(() => {
		if (value === undefined && options && options.length > 0) {
			onChange(options[0].value as string);
		}
	}, [fieldSchema, onChange, options, value]);

	return (
		<Select
			loading={isFetchingExecutors}
			style={{ width: fieldWidthPx }}
			value={value}
			onChange={(val) => onChange(val)}
			options={options}
		/>
	);
};

export default FieldExecutorPickerEditor;
