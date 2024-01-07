import { useQuery } from '@tanstack/react-query';
import { Button, ConfigProvider, Tooltip, theme as antdTheme } from 'antd';
import { actionDefinitionGetAll, definitionKeys } from 'api/actionDefinitionApi';
import { ActionExecutorDTO } from 'api/actionExecutorApi';
import IconButton from 'components/UI/IconButton/IconButton';
import dayjs from 'dayjs';
import { rgba } from 'polished';
import React from 'react';
import { AiFillSetting } from 'react-icons/ai';
import { IoIosPlay } from 'react-icons/io';
import { themeDark } from 'setup/AppThemeProvider';
import { styled, useTheme } from 'styled-components';

interface ActionExecutorItemProps {
	actionExecutor: ActionExecutorDTO;
	onClickedSettings: (actionExecutor: ActionExecutorDTO) => void;
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
	onClickedSettings
}) => {
	const { data: actionDefinition } = useQuery({
		queryKey: definitionKeys.list(),
		queryFn: ({ signal }) => actionDefinitionGetAll(signal),
		select: (data) => data.find((a) => a.id === actionExecutor.actionDefinitionId)
	});
	const { darkAlgorithm } = antdTheme;

	const noInputs = actionExecutor.actionData.inputs.length === 0;
	const theme = useTheme();
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
					<Tooltip title="Settings">
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
							Type: <b>{actionExecutor.runPeriod}</b>
						</div>
						<div>
							Inputs: <b>{actionExecutor.actionData.inputs.length}</b>
						</div>
						<div>
							In queue: <b>{0}</b>
						</div>
						<div>
							Last run:{' '}
							<b>
								{actionExecutor.lastRunDate ? dayjs(actionExecutor.lastRunDate).toNow() : 'never'}
							</b>
						</div>
					</div>
					<Tooltip
						title={
							noInputs
								? 'No inputs to execute'
								: !actionExecutor.isValid
									? 'Settings are invalid'
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
								marginBottom: '7px'
							}}
						>
							<IoIosPlay />
							Run
						</IconButton>
					</Tooltip>
					{/* {actionExecutor.actionExecutorName} ({actionExecutor.id}) */}
				</Backdrop>
			</Container>
		</ConfigProvider>
	);
};

export default ActionExecutorItem;
