import { Button, Input } from 'antd';
import { ActionExecutorExtendedDTO, RunPeriod } from 'api/actionExecutorApi';
import dayjs from 'dayjs';
import React, { useState } from 'react';
import { styled } from 'styled-components';
import { Updater } from 'use-immer';
import FieldEditor from '../InputsEditor/InputEditor/FieldEditor/FieldEditor';

interface ExecutorSettingsProps {
	executor: ActionExecutorExtendedDTO;
	updateExecutor: Updater<ActionExecutorExtendedDTO | undefined>;
}
const Row = styled.div`
	display: flex;
	flex-direction: row;
	align-items: center;
	gap: 5px;
	flex-wrap: wrap;
`;
const Column = styled.div`
	display: flex;
	flex-direction: column;
	gap: 5px;
`;
const ContentItem = styled.div`
	display: flex;
	align-items: center;
	gap: 8px;
`;

const TwoColumnsGrid = styled.div`
	display: grid;
	grid-template-columns: auto 1fr;
	row-gap: 5px;
`;

const ExecutorSettings: React.FC<ExecutorSettingsProps> = ({ executor, updateExecutor }) => {
	const [isEditingName, setIsEditingName] = useState(false);

	return (
		<Row style={{ gap: '20px' }}>
			<img
				style={{ width: '190px', height: '190px', borderRadius: '12px' }}
				src={executor.actionDefinition.actionDefinitionIcon}
			></img>
			<Column>
				<ContentItem style={{ fontSize: '24px', fontWeight: '100', marginBottom: '5px' }}>
					{isEditingName ? (
						<>
							<Input
								style={{ fontSize: '24px', fontWeight: '100' }}
								autoFocus
								onBlur={() => setIsEditingName(false)}
								value={executor.actionExecutorName}
								onChange={(e) =>
									updateExecutor((a) => {
										a!.actionExecutorName = e.target.value;
									})
								}
								onPressEnter={() => setIsEditingName(false)}
							/>
						</>
					) : (
						<>
							{executor.actionExecutorName}
							<Button onClick={() => setIsEditingName((p) => !p)}>Change</Button>
						</>
					)}
				</ContentItem>
				<ContentItem>
					ID: <b>{executor.id}</b>
				</ContentItem>
				<ContentItem>
					Created: <b>{dayjs(executor.createdDate).format('L LTS')}</b>
				</ContentItem>
				<ContentItem>
					Modified: <b>{dayjs(executor.modifiedDate).format('L LTS')}</b>
				</ContentItem>
				<ContentItem>
					In queue: <b>0</b>
				</ContentItem>
				<ContentItem>
					Last run: <b>{executor.lastRunDate ? dayjs(executor.lastRunDate).toNow() : 'never'}</b>
				</ContentItem>
			</Column>
			<Column style={{ margin: 'auto' }}>
				<TwoColumnsGrid>
					<FieldEditor
						fieldSchema={{
							name: 'Preserve inputs on execute',
							description: 'Preserve inputs after executed',
							isOptional: false,
							type: 'Boolean'
						}}
						onChange={(value) =>
							updateExecutor((a) => {
								if (a) {
									a.preserveExecutedInputs = value === 'true';
								}
							})
						}
						value={executor.preserveExecutedInputs ? 'true' : 'false'}
					/>
					<FieldEditor
						fieldSchema={{
							name: 'Run type',
							description: 'How do you want to run the actions?',
							isOptional: false,
							type: 'Set',
							setOptions: [
								{ display: 'Manual', value: 'Manual' },
								{ display: 'Everyday', value: 'Everyday' },
								{ display: 'Loop', value: 'Loop' },
								{ display: 'TimePeriod', value: 'TimePeriod' }
							]
						}}
						onChange={(value) =>
							updateExecutor((a) => {
								if (a) {
									a.runPeriod = value as RunPeriod;
									if (!a.timeIntervalSeconds) {
										a.timeIntervalSeconds = 300;
									}
								}
							})
						}
						value={executor.runPeriod}
					/>
					{executor.runPeriod === 'TimePeriod' && (
						<FieldEditor
							fieldSchema={{
								name: 'Run interval (sec)',
								description: 'How often actions should be executed?',
								isOptional: false,
								type: 'Number'
							}}
							onChange={(value) =>
								updateExecutor((a) => {
									if (a) {
										a.timeIntervalSeconds = parseInt(value ?? '300');
									}
								})
							}
							value={executor.timeIntervalSeconds?.toString() ?? '300'}
						/>
					)}
					<FieldEditor
						fieldSchema={{
							name: 'Run on finish',
							description: 'What executor run when finish?',
							isOptional: true,
							type: 'ExecutorPicker'
						}}
						onChange={(value) =>
							updateExecutor((a) => {
								if (a) {
									a.actionExecutorOnFinishId =
										value === '' || value === undefined ? undefined : parseInt(value);
								}
							})
						}
						value={
							executor.actionExecutorOnFinishId === undefined
								? ''
								: executor.actionExecutorOnFinishId?.toString()
						}
					/>
				</TwoColumnsGrid>
			</Column>
		</Row>
	);
};

export default ExecutorSettings;
