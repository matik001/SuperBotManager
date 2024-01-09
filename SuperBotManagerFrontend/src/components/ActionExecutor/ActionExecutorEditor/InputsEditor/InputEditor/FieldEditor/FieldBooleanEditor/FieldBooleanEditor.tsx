import { Switch } from 'antd';
import React from 'react';
import { InnerFieldEditorProps } from '../FieldEditor';

const FieldBooleanEditor: React.FC<InnerFieldEditorProps> = ({ fieldSchema, onChange, value }) => {
	return (
		<Switch
			value={value.value !== undefined ? value.value === 'true' : undefined}
			onChange={(val) => onChange({ ...value, value: val ? 'true' : 'false' })}
		></Switch>
	);
};

export default FieldBooleanEditor;
