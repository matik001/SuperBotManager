import { useQuery } from '@tanstack/react-query';
import { Select, SelectProps } from 'antd';
import { actionExecutorGetAll, executorKeys } from 'api/actionExecutorApi';
import React, { useEffect, useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import { InnerFieldEditorProps } from '../FieldEditor';

const FieldExecutorPickerEditor: React.FC<InnerFieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	const { data: executors, isFetching: isFetchingExecutors } = useQuery({
		queryKey: executorKeys.list(),
		queryFn: ({ signal }) => actionExecutorGetAll(signal)
	});
	const { t } = useTranslation();
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
					label: t('None'),
					value: ''
				},
				...newOptions!
			];
		}
		return newOptions;
	}, [executors, fieldSchema.isOptional, t]);

	useEffect(() => {
		if (value.value === undefined && options && options.length > 0) {
			onChange({ ...value, value: options[0].value as string });
		}
	}, [fieldSchema, onChange, options, value]);

	return (
		<Select
			disabled={value.disabled}
			loading={isFetchingExecutors}
			style={{ width: fieldWidthPx }}
			value={value.value}
			onChange={(val) => onChange({ ...value, value: val })}
			options={options}
		/>
	);
};

export default FieldExecutorPickerEditor;
