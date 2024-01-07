import { DatePicker } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import dayjs from 'dayjs';
import React from 'react';

interface FieldDateTimeEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
}

const FieldDateTimeEditor: React.FC<FieldDateTimeEditorProps> = ({
	fieldSchema,
	onChange,
	value
}) => {
	return (
		<DatePicker
			value={value ? dayjs(value) : undefined}
			showTime
			onChange={(date) => onChange(date?.toISOString())}
		></DatePicker>
	);
};

export default FieldDateTimeEditor;
