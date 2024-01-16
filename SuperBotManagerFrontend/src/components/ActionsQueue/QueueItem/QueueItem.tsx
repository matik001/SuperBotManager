import { LoadingOutlined } from '@ant-design/icons';
import { Divider, Input, Space, Spin } from 'antd';
import { ActionDTO } from 'api/actionApi';
import { ActionExecutorExtendedDTO } from 'api/actionExecutorApi';
import dayjs from 'dayjs';
import { motion } from 'framer-motion';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { styled, useTheme } from 'styled-components';

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
`;
const InputOutputContainer = styled.div`
	display: grid;
	grid-template-columns: 1fr 1fr;
	gap: 30px;
`;
const InputOutputTitle = styled.div`
	grid-column: span 2;
	text-align: center;
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
				<Space style={{ width: '250px' }}>
					<img
						style={{ width: '30px', height: '30px', borderRadius: '6px' }}
						src={executor.actionDefinition.actionDefinitionIcon}
					></img>
					<h4>{executor.actionExecutorName}</h4>
				</Space>

				<div style={{ marginRight: 'auto' }}>
					Forwared to: <b>{executor.actionExecutorOnFinishId}</b>
				</div>
				<div>
					Created: <b>{dayjs(action.createdDate).toNow()}</b>
				</div>
			</HeadInfo>

			<motion.div
				style={{ overflow: 'hidden' }}
				exit={{
					display: isOpen ? 'block' : 'none'
				}}
				animate={{ height: isOpen ? '100%' : '0px' }}
				transition={{ ease: 'easeOut', duration: 0.3 }}
			>
				<Divider />
				<InputOutputContainer>
					<InputContainer>
						<InputOutputTitle>
							{Object.keys(action.actionData.input).length === 0 ? 'No input' : 'Input'}
						</InputOutputTitle>

						{Object.entries(action.actionData.input).map(([key, value]) => {
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
					<InputContainer>
						<InputOutputTitle>
							{Object.keys(action.actionData.output).length === 0 ? 'No output' : 'Output'}
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
