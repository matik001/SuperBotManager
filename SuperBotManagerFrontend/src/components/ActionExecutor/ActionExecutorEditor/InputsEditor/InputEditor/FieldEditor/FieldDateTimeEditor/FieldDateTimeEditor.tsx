import { DatePicker } from 'antd';
import dayjs from 'dayjs';
import React from 'react';
import { InnerFieldEditorProps } from '../FieldEditor';

const FieldDateTimeEditor: React.FC<InnerFieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	return (
		<DatePicker
			style={{ width: fieldWidthPx }}
			value={value.value ? dayjs(value.value) : undefined}
			showTime
			onChange={(date) => onChange({ ...value, value: date?.toISOString() ?? null })}
		></DatePicker>
	);
};

export default FieldDateTimeEditor;
