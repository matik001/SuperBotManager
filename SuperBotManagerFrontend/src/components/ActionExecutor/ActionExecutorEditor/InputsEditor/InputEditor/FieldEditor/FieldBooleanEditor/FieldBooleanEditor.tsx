import { Switch } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import React from 'react';

interface FieldBooleanEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
}

const FieldBooleanEditor: React.FC<FieldBooleanEditorProps> = ({
	fieldSchema,
	onChange,
	value
}) => {
	return (
		<Switch
			value={value !== undefined ? value === 'true' : undefined}
			onChange={(val) => onChange(val ? 'true' : 'false')}
		></Switch>
	);
};

export default FieldBooleanEditor;
