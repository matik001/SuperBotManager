import { Input } from 'antd';
import React from 'react';
import { MASK_ENCRYPTED } from 'utils/executorUtils';
import { InnerFieldEditorProps } from '../FieldEditor';

const FieldSecretEditor: React.FC<InnerFieldEditorProps> = ({
	fieldSchema,
	onChange,
	value,
	fieldWidthPx
}) => {
	return (
		<Input.Password
			style={{ width: fieldWidthPx }}
			placeholder="Provide a value"
			value={value.isEncrypted ? MASK_ENCRYPTED : value.value}
			onChange={(e) => {
				if (value.isEncrypted) {
					onChange({ ...value, isEncrypted: false, value: '' });
				} else {
					onChange({ ...value, isEncrypted: false, value: e.target.value });
				}
			}}
		></Input.Password>
	);
};

export default FieldSecretEditor;
