import { Form, Input } from 'antd';
import { CSSProperties } from 'react';
import { Control, Controller, FieldValues, UseControllerProps } from 'react-hook-form';
export type RulesType = UseControllerProps['rules'];
export interface ValidatedInputProps<T extends FieldValues> {
	name: string;
	// errors: FieldErrors<FieldValues>;
	control: Control<T, any>;
	rules: RulesType;
	style?: CSSProperties;
	isPassword?: boolean;
}

const ValidatedInput = <T extends FieldValues>({
	// errors,
	name,
	control,
	rules,
	style,
	isPassword = false
}: ValidatedInputProps<T>) => {
	return (
		<Controller
			name={name}
			defaultValue=""
			control={control as Control<FieldValues, any>}
			rules={rules}
			render={({
				fieldState: { invalid, error },
				field: { name, onBlur, onChange, ref, value, disabled }
			}) => {
				return (
					<Form.Item
						validateStatus={invalid ? 'warning' : undefined}
						help={error?.message?.toString()}
						hasFeedback
						style={style}
					>
						{isPassword ? (
							<Input.Password
								value={value}
								onChange={onChange}
								onBlur={onBlur}
								ref={ref}
								disabled={disabled}
								name={name}
							/>
						) : (
							<Input
								value={value}
								onChange={onChange}
								onBlur={onBlur}
								ref={ref}
								disabled={disabled}
								name={name}
							/>
						)}
					</Form.Item>
				);
			}}
		/>
	);
};

export default ValidatedInput;
