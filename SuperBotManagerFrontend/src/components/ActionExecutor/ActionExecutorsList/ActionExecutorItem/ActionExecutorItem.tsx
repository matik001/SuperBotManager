import { useQuery } from '@tanstack/react-query';
import { Button, ConfigProvider, Tooltip, theme as antdTheme } from 'antd';
import { actionGetAll, actionKeys } from 'api/actionApi';
import { actionDefinitionGetAll, definitionKeys } from 'api/actionDefinitionApi';
import { ActionExecutorDTO } from 'api/actionExecutorApi';
import IconButton from 'components/UI/IconButton/IconButton';
import dayjs from 'dayjs';
import { rgba } from 'polished';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { AiFillSetting } from 'react-icons/ai';
import { IoIosPlay } from 'react-icons/io';
import { themeDark } from 'setup/AppThemeProvider';
import { styled, useTheme } from 'styled-components';

interface ActionExecutorItemProps {
	actionExecutor: ActionExecutorDTO;
	onClickedSettings: (actionExecutor: ActionExecutorDTO) => void;
	onRun: () => void;
	isQueueing: boolean;
}

const Container = styled.div<{ $imgUrl: string }>`
	width: 200px;
	height: 200px;
	background-image: url(${(a) => a.$imgUrl});
	background-size: cover;
	border-radius: 14px;
	position: relative;
	overflow: hidden;
	color: white;
`;
const Backdrop = styled.div`
	background-color: ${(a) => rgba('black', 0.75)};
	position: relative;
	display: flex;

	flex-direction: column;
	align-items: center;
	overflow: hidden;
	padding: 10px;

	width: 100%;
	height: 100%;
`;
const ActionExecutorItem: React.FC<ActionExecutorItemProps> = ({
	actionExecutor,
	onClickedSettings,
	onRun,
	isQueueing
}) => {
	const { data: actionDefinition } = useQuery({
		queryKey: definitionKeys.list(),
		queryFn: ({ signal }) => actionDefinitionGetAll(signal),
		select: (data) => data.find((a) => a.id === actionExecutor.actionDefinitionId)
	});
	const { data: actions } = useQuery({
		queryKey: actionKeys.list(),
		queryFn: ({ signal }) => actionGetAll(signal)
	});
	const amountInQueue =
		actions?.filter(
			(a) =>
				a.actionExecutorId === actionExecutor.id &&
				(a.actionStatus === 'InProgress' || a.actionStatus === 'Pending')
		).length ?? 0;

	const { darkAlgorithm } = antdTheme;

	const noInputs = actionExecutor.actionData.inputs.length === 0;
	const theme = useTheme();
	const { t, i18n } = useTranslation();

	return (
		<ConfigProvider
			theme={{
				token: {
					colorText: 'white',
					colorBgBase: themeDark.bgColor
				},
				algorithm: darkAlgorithm
			}}
		>
			<Container $imgUrl={actionDefinition?.actionDefinitionIcon ?? ''}>
				<Backdrop>
					<Tooltip title={t('Settings')}>
						<Button
							styles={{
								icon: {
									fontSize: '20px'
								}
							}}
							type="text"
							shape="circle"
							icon={<AiFillSetting />}
							onClick={() => onClickedSettings(actionExecutor)}
							style={{ position: 'absolute', right: '16px', bottom: '16px' }}
						/>
					</Tooltip>
					<div
						title={actionExecutor.actionExecutorName}
						style={{
							fontSize: '20px',
							textOverflow: 'ellipsis',
							whiteSpace: 'nowrap',
							overflow: 'hidden',
							marginTop: '10px',
							maxWidth: '90%',
							marginBottom: '9px'
						}}
					>
						{actionExecutor.actionExecutorName}
					</div>
					<div>
						<div>
							{t('Type')}: <b>{t(actionExecutor.runMethod)}</b>
						</div>
						<div>
							{t('Inputs')}: <b>{actionExecutor.actionData.inputs.length}</b>
						</div>
						<div>
							{t('In queue')}: <b>{amountInQueue}</b>
						</div>
						<div>
							{t('Last run')}:{' '}
							<b style={{ fontSize: '12px' }}>
								{actionExecutor.lastRunDate
									? t('{{x}} ago', { x: dayjs(actionExecutor.lastRunDate).toNow(true) })
									: t('never')}
							</b>
						</div>
					</div>
					{actionExecutor.runMethod === 'Manual' ? (
						<Tooltip
							title={
								noInputs
									? t('No inputs to execute')
									: !actionExecutor.isValid
										? t('Settings are invalid')
										: undefined
							}
							placement="bottom"
							color="red"
						>
							<IconButton
								disabled={!actionExecutor.isValid}
								type="primary"
								shape="round"
								style={{
									marginTop: 'auto',
									marginBottom: '7px',
									marginLeft: i18n.language === 'pl' ? '-10px' : undefined
								}}
								loading={isQueueing}
								onClick={onRun}
							>
								<IoIosPlay />
								{t('Run')}
							</IconButton>
						</Tooltip>
					) : (
						<div
							style={{
								marginTop: 'auto',
								marginBottom: '14px',
								fontWeight: 'bold'
							}}
						>
							{t('Automatic')}
						</div>
					)}
					{/* {actionExecutor.actionExecutorName} ({actionExecutor.id}) */}
				</Backdrop>
			</Container>
		</ConfigProvider>
	);
};

export default ActionExecutorItem;
