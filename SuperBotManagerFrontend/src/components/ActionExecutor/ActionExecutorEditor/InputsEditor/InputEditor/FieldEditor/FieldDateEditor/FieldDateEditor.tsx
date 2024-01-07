import { DatePicker } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import dayjs from 'dayjs';
import React from 'react';

interface FieldDateEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
}

const FieldDateEditor: React.FC<FieldDateEditorProps> = ({ fieldSchema, onChange, value }) => {
	return (
		<DatePicker
			value={value ? dayjs(value) : undefined}
			onChange={(date) => onChange(date?.toISOString())}
		></DatePicker>
	);
};

export default FieldDateEditor;
