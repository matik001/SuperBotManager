import { DatePicker } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import dayjs from 'dayjs';
import React from 'react';

interface FieldDateEditorProps {
	fieldSchema: FieldInfo;
	value: string | undefined;
	onChange: (newVal: string | undefined) => void;
	fieldWidthPx?: number;
}

const FieldDateEditor: React.FC<FieldDateEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	return (
		<DatePicker
			style={{ width: fieldWidthPx }}
			value={value ? dayjs(value) : undefined}
			onChange={(date) => onChange(date?.toISOString())}
		></DatePicker>
	);
};

export default FieldDateEditor;
