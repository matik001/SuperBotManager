import { Button, Spin } from 'antd';
import ValidatedInput from 'components/UI/ValidatedInput/ValidatedInput';
import React from 'react';
import { useForm } from 'react-hook-form';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';
import useAuthStore from 'store/authStore';
import { styled } from 'styled-components';

interface SignUpFormProps {}

const SignupPanel = styled.div`
	padding: 10px 60px 30px 60px;
	background-color: ${(p) => p.theme.secondaryColor};
	width: 600px;
	margin: 100px auto;
	max-width: 90%;
`;
const Title = styled.div`
	font-size: 36px;
	padding: 20px 0 40px 0;
	font-weight: 300;
`;
interface FormData {
	Email: string;
	Username: string;
	Password: string;
	ConfirmPassword: string;
}
const SignUpForm: React.FC<SignUpFormProps> = ({}) => {
	const signup = useAuthStore((a) => a.signup);
	const isLoading = useAuthStore((a) => a.isLoading);
	const navigate = useNavigate();
	const {
		handleSubmit,
		formState: { errors },
		watch,
		control
	} = useForm<FormData>();
	const onSubmit = (data: FormData) => {
		signup({
			email: data.Email,
			password: data.Password,
			userName: data.Username
		});
	};
	const { t } = useTranslation();
	return (
		<SignupPanel>
			<Spin spinning={isLoading}>
				<Title>{t('Sign up')}</Title>
				<form onSubmit={handleSubmit(onSubmit)}>
					<div style={{ marginBottom: '9px' }}>{t('Email')}: </div>
					<ValidatedInput
						control={control}
						name="Email"
						style={{ marginBottom: '24px' }}
						rules={{
							required: t('The field cannot be empty'),
							pattern: {
								value: /^\S+@\S+$/i,
								message: t('You must provide a valid email')
							}
						}}
					/>

					<div style={{ marginBottom: '9px' }}>{t('User name')}: </div>
					<ValidatedInput
						control={control}
						name="Username"
						style={{ marginBottom: '24px' }}
						rules={{
							required: t('The field cannot be empty')
						}}
					/>

					<div style={{ marginBottom: '9px' }}>{t('Password')}: </div>
					<ValidatedInput
						isPassword
						control={control}
						name="Password"
						style={{ marginBottom: '24px' }}
						rules={{
							required: t('The field cannot be empty'),
							minLength: {
								value: 6,
								message: t('Password must have at least 6 characters')
							}
						}}
					/>

					<div style={{ marginBottom: '9px' }}>{t('Confirm password')}: </div>
					<ValidatedInput
						isPassword
						control={control}
						name="ConfirmPassword"
						style={{ marginBottom: '32px' }}
						rules={{
							required: t('The field cannot be empty'),
							minLength: {
								value: 6,
								message: t('Password must have at least 6 characters')
							},
							validate: (value) => value === watch('Password') || t('Passwords dont match')
						}}
					/>

					<Button type="primary" htmlType="submit">
						{t('Sign up')}
					</Button>
					<Button type="link" onClick={() => navigate('/signin')}>
						{t('Already have an account? Sign in.')}
					</Button>
				</form>
			</Spin>
		</SignupPanel>
	);
};

export default SignUpForm;
