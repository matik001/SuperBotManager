import { LoadingOutlined } from '@ant-design/icons';
import { useMutation, useQueryClient } from '@tanstack/react-query';
import { Divider, Input, Space, Spin } from 'antd';
import { ActionDTO, actionKeys, actionPut } from 'api/actionApi';
import { ActionExecutorExtendedDTO } from 'api/actionExecutorApi';
import IconButton from 'components/UI/IconButton/IconButton';
import dayjs from 'dayjs';
import { motion } from 'framer-motion';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { MdOutlineCancel } from 'react-icons/md';
import { styled, useTheme } from 'styled-components';
import { MASK_ENCRYPTED } from 'utils/executorUtils';

interface QueueItemProps {
	action: ActionDTO;
	executor: ActionExecutorExtendedDTO;
}

const Container = styled.div`
	transition: 0.4s all;
	padding: 20px;
	border-radius: 7px;
	background-color: ${(p) => p.theme.secondaryBgColor};
	margin: 10px;
	border: 1px solid ${(p) => p.theme.bgColor3};
	cursor: pointer;
	&:hover {
		border: 1px solid ${(p) => p.theme.primaryColor};
	}
`;
const InputContainer = styled.div`
	display: grid;
	grid-template-columns: auto 1fr;
	column-gap: 20px;
	row-gap: 4px;
	align-items: center;
	font-size: 14px;
	grid-auto-rows: min-content;
`;
const InputOutputContainer = styled.div`
	display: grid;
	grid-template-columns: 1fr 1fr;
	gap: 40px;
`;
const InputOutputTitle = styled.div`
	grid-column: span 2;
	font-size: 26px;
	font-weight: 300;
	margin-bottom: 14px;
`;
const HeadInfo = styled.div`
	display: flex;
	gap: 30px;
	align-items: center;
	justify-content: space-between;
`;
const QueueItem: React.FC<QueueItemProps> = ({ action, executor }) => {
	const [isOpen, setIsOpen] = useState(false);
	const theme = useTheme();
	const { t } = useTranslation();
	const queryClient = useQueryClient();
	const cancelMutation = useMutation({
		mutationFn: async () => {
			await actionPut({
				...action,
				actionStatus: 'Canceled'
			});
		},
		onSuccess: () => {
			queryClient.invalidateQueries({
				queryKey: actionKeys.list()
			});
			queryClient.invalidateQueries({
				queryKey: actionKeys.one(action.id)
			});
		}
	});
	return (
		<Container onClick={() => setIsOpen((p) => !p)}>
			<HeadInfo>
				<div
					style={{
						display: 'flex',
						flexFlow: 'row nowrap',
						alignItems: 'center',
						width: '120px',
						color:
							action.actionStatus === 'Error'
								? theme.errorColor
								: action.actionStatus === 'Pending'
									? theme.infoColor
									: action.actionStatus === 'Finished'
										? theme.successColor
										: theme.primaryColor
					}}
				>
					{action.actionStatus === 'InProgress' && (
						<Spin
							indicator={<LoadingOutlined style={{ fontSize: 24, marginRight: '10px' }} spin />}
						/>
					)}
					{t(action.actionStatus)}
				</div>
				<Space style={{ width: '300px' }} align="center">
					<img
						style={{ width: '30px', height: '30px', borderRadius: '6px' }}
						src={executor.actionDefinition.actionDefinitionIcon}
					></img>
					<b>{executor.actionExecutorName}</b>{' '}
					<span style={{ marginLeft: '-9px', fontSize: '12px', display: 'block' }}>
						({action.id})
					</span>
				</Space>

				<div style={{ width: '240px' }}>
					{t('Start type')}: <b>{t(action.runStartType)}</b>
				</div>
				{action.forwardedFromActionId && (
					<div>
						Previous action id: <b>{action.forwardedFromActionId}</b>
					</div>
				)}
				{(action.actionStatus === 'InProgress' || action.actionStatus === 'Pending') && (
					<IconButton
						danger
						type="primary"
						onClick={(e) => {
							e.stopPropagation();
							cancelMutation.mutate();
						}}
					>
						<MdOutlineCancel size={18}></MdOutlineCancel>
						{t('Cancel')}
					</IconButton>
				)}
				<div style={{ marginLeft: 'auto' }}>
					{t('Created')}: <b>{t('{{x}} ago', { x: dayjs.utc(action.createdDate).toNow(true) })}</b>
				</div>
			</HeadInfo>

			<motion.div
				style={{ overflow: 'hidden' }}
				initial={{ height: '0px' }}
				animate={{ height: isOpen ? '100%' : '0px' }}
				transition={{ ease: 'easeOut', duration: 0.3 }}
			>
				<Divider />
				<InputOutputContainer>
					<InputContainer>
						<InputOutputTitle>
							{Object.keys(action.actionData.input).length === 0 ? t('No input') : t('Input')}
						</InputOutputTitle>

						{Object.entries(action.actionData.input).map(([key, value]) => {
							const fieldDef = executor.actionDefinition.actionDataSchema.inputSchema.find(
								(a) => a.name === key
							);
							return (
								<>
									<div>{key}</div>
									{fieldDef?.type === 'Secret' ? (
										<Input.Password
											width="200px"
											readOnly
											onClick={(e) => e.stopPropagation()}
											value={MASK_ENCRYPTED}
										/>
									) : (
										<Input
											onClick={(e) => e.stopPropagation()}
											width="200px"
											readOnly
											value={value}
										></Input>
									)}
								</>
							);
						})}
					</InputContainer>
					<InputContainer>
						<InputOutputTitle>
							{Object.keys(action.actionData.output).length === 0 ? t('No output') : t('Output')}
						</InputOutputTitle>
						{Object.entries(action.actionData.output).map(([key, value]) => {
							return (
								<>
									<div>{key}</div>
									<Input
										onClick={(e) => e.stopPropagation()}
										width="200px"
										readOnly
										value={value}
									></Input>
								</>
							);
						})}
					</InputContainer>
				</InputOutputContainer>
			</motion.div>
		</Container>
	);
};

export default QueueItem;
