import { Form, Tooltip } from 'antd';
import { FieldInfo, FieldType } from 'api/actionDefinitionApi';
import React, { ReactNode, useMemo } from 'react';
import { styled } from 'styled-components';
import FieldBooleanEditor from './FieldBooleanEditor/FieldBooleanEditor';
import FieldDateEditor from './FieldDateEditor/FieldDateEditor';
import FieldDateTimeEditor from './FieldDateTimeEditor/FieldDateTimeEditor';
import FieldNumberEditor from './FieldNumberEditor/FieldNumberEditor';
import FieldSetEditor from './FieldSetEditor/FieldSetEditor';
import FieldStringEditor from './FieldStringEditor/FieldStringEditor';

interface FieldEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
}

const Container = styled.div``;
const FieldEditor: React.FC<FieldEditorProps> = ({ fieldSchema, onChange, value }) => {
	const invalidMessage = useMemo(() => {
		if (!fieldSchema.isOptional && (value === undefined || value.length === 0))
			return 'Field cannot be empty';
		return undefined;
	}, [fieldSchema.isOptional, value]);
	return (
		<>
			<div style={{ marginRight: '15px', alignSelf: 'center' }}>{fieldSchema.name}</div>
			<Tooltip placement="right" color="volcano" title={invalidMessage}>
				<Tooltip title={fieldSchema.description}>
					<div
						style={{
							width: '200px'
						}}
					>
						<Form.Item
							// noStyle
							validateStatus={invalidMessage ? 'warning' : undefined}
							hasFeedback
							style={{
								marginBottom: 0
							}}
						>
							{
								(
									{
										String: (
											<FieldStringEditor
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Number: (
											<FieldNumberEditor
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Date: (
											<FieldDateEditor
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										DateTime: (
											<FieldDateTimeEditor
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Boolean: (
											<FieldBooleanEditor
												fieldSchema={fieldSchema}
												onChange={onChange}
												value={value}
											/>
										),
										Set: (
											<FieldSetEditor fieldSchema={fieldSchema} onChange={onChange} value={value} />
										)
									} as Record<FieldType, ReactNode>
								)[fieldSchema.type]
							}
						</Form.Item>
					</div>
				</Tooltip>
			</Tooltip>
		</>
	);
};

export default FieldEditor;
