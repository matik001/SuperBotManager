import { Form, Tooltip } from 'antd';
import { FieldInfo, FieldType } from 'api/actionDefinitionApi';
import { FieldValue } from 'api/actionExecutorApi';
import React, { ReactNode, useEffect, useMemo } from 'react';
import { styled } from 'styled-components';
import FieldBooleanEditor from './FieldBooleanEditor/FieldBooleanEditor';
import FieldDateEditor from './FieldDateEditor/FieldDateEditor';
import FieldDateTimeEditor from './FieldDateTimeEditor/FieldDateTimeEditor';
import FieldExecutorPickerEditor from './FieldExecutorPickerEditor/FieldExecutorPickerEditor';
import FieldNumberEditor from './FieldNumberEditor/FieldNumberEditor';
import FieldSecretEditor from './FieldSecretEditor/FieldSecretEditor';
import FieldSetEditor from './FieldSetEditor/FieldSetEditor';
import FieldStringEditor from './FieldStringEditor/FieldStringEditor';

interface FieldEditorProps {
	fieldSchema: FieldInfo;
	value: FieldValue | undefined;
	onChange: (newValue: FieldValue | undefined) => void;
	fieldWidthPx?: number;
}
export interface InnerFieldEditorProps {
	fieldSchema: FieldInfo;
	value: FieldValue;
	onChange: (newValue: FieldValue) => void;
	fieldWidthPx?: number;
}

const Container = styled.div``;
const FieldEditor: React.FC<FieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx = 200
}) => {
	const invalidMessage = useMemo(() => {
		if (
			!fieldSchema.isOptional &&
			(value === undefined || value.value === null || value.value.length === 0)
		)
			return 'Field cannot be empty';
		return undefined;
	}, [fieldSchema.isOptional, value]);
	useEffect(() => {
		const newValid = !invalidMessage;
		if (value?.isValid === newValid) return;
		if (value === undefined) {
			onChange({
				isEncrypted: false,
				isValid: true,
				value: null
			});
		} else {
			onChange({
				...value,
				isValid: newValid
			});
		}
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [invalidMessage, value?.value]);

	const valueNotUndefined = useMemo(
		() =>
			value === undefined
				? ({ isEncrypted: false, isValid: false, value: null } as FieldValue)
				: value,
		[value]
	);

	useEffect(() => {
		if (value === undefined) onChange(valueNotUndefined);
	}, [onChange, value, valueNotUndefined]);

	return (
		<>
			<div style={{ marginRight: '15px', alignSelf: 'center' }}>{fieldSchema.name}</div>
			<Tooltip placement="right" color="volcano" title={invalidMessage}>
				<div
				// style={{
				// 	width: '200px'
				// }}
				>
					<Tooltip title={fieldSchema.description}>
						<Form.Item
							validateStatus={invalidMessage ? 'warning' : undefined}
							hasFeedback
							style={{
								marginBottom: 0,
								width: 'min-content'
							}}
						>
							{
								(
									{
										String: (
											<FieldStringEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										),
										Secret: (
											<FieldSecretEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										),
										Number: (
											<FieldNumberEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										),
										Date: (
											<FieldDateEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										),
										DateTime: (
											<FieldDateTimeEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										),
										Boolean: (
											<FieldBooleanEditor
												// fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										),
										Set: (
											<FieldSetEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										),
										ExecutorPicker: (
											<FieldExecutorPickerEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={valueNotUndefined}
											/>
										)
									} as Record<FieldType, ReactNode>
								)[fieldSchema.type]
							}
						</Form.Item>
					</Tooltip>
				</div>
			</Tooltip>
		</>
	);
};

export default FieldEditor;
