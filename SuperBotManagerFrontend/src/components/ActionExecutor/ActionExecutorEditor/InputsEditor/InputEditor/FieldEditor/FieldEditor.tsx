import { Form, Tooltip } from 'antd';
import { FieldInfo, FieldType } from 'api/actionDefinitionApi';
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
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
	isValid?: boolean;
	onChangeValidation?: (inputName: string, isValid: boolean) => void;
	fieldWidthPx?: number;
}

const Container = styled.div``;
const FieldEditor: React.FC<FieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	isValid,
	onChangeValidation,
	fieldWidthPx = 200
}) => {
	const invalidMessage = useMemo(() => {
		if (!fieldSchema.isOptional && (value === undefined || value.length === 0))
			return 'Field cannot be empty';
		return undefined;
	}, [fieldSchema.isOptional, value]);
	useEffect(() => {
		const newValid = !invalidMessage;
		if (!onChangeValidation || isValid === newValid) return;
		onChangeValidation(fieldSchema.name, newValid);
		// eslint-disable-next-line react-hooks/exhaustive-deps
	}, [invalidMessage]);
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
												value={value}
											/>
										),
										Secret: (
											<FieldSecretEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Number: (
											<FieldNumberEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Date: (
											<FieldDateEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										DateTime: (
											<FieldDateTimeEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Boolean: (
											<FieldBooleanEditor
												// fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Set: (
											<FieldSetEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										ExecutorPicker: (
											<FieldExecutorPickerEditor
												fieldWidthPx={fieldWidthPx}
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
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
