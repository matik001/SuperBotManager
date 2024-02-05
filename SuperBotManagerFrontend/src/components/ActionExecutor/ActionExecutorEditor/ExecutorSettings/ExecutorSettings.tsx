import { Button, Input } from 'antd';
import { ActionExecutorExtendedDTO, RunMethod } from 'api/actionExecutorApi';
import dayjs from 'dayjs';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
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
	const { t } = useTranslation();
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
							<Button onClick={() => setIsEditingName((p) => !p)}>{t('Change')}</Button>
						</>
					)}
				</ContentItem>
				<ContentItem>
					ID: <b>{executor.id}</b>
				</ContentItem>
				<ContentItem>
					{t('Created')}: <b>{dayjs(executor.createdDate).format('L LTS')}</b>
				</ContentItem>
				<ContentItem>
					{t('Modified')}: <b>{dayjs(executor.modifiedDate).format('L LTS')}</b>
				</ContentItem>
				<ContentItem>
					{t('In queue')}: <b>0</b>
				</ContentItem>
				<ContentItem>
					{t('Last run')}:{' '}
					<b>{executor.lastRunDate ? dayjs(executor.lastRunDate).toNow() : t('never')}</b>
				</ContentItem>
			</Column>
			<Column style={{ margin: 'auto' }}>
				<TwoColumnsGrid>
					<FieldEditor
						fieldSchema={{
							name: t('Preserve inputs on execute'),
							description: t('Preserve inputs on execute'),
							isOptional: false,
							type: 'Boolean'
						}}
						onChange={(value) =>
							updateExecutor((a) => {
								if (a) {
									a.preserveExecutedInputs = value?.value === 'true';
								}
							})
						}
						value={{
							isEncrypted: false,
							isValid: true,
							value: executor.preserveExecutedInputs ? 'true' : 'false'
						}}
					/>
					<FieldEditor
						fieldSchema={{
							name: t('Run type'),
							description: t('How do you want to run the actions?'),
							isOptional: false,
							type: 'Set',
							setOptions: [
								{ display: t('Manual'), value: 'Manual' },
								{ display: t('Automatic'), value: 'Automatic' }
							]
						}}
						onChange={(value) =>
							updateExecutor((a) => {
								if (a) {
									a.runMethod = value?.value as RunMethod;
								}
							})
						}
						value={{ isEncrypted: false, isValid: true, value: executor.runMethod }}
					/>
					<FieldEditor
						fieldSchema={{
							name: t('Run on finish'),
							description: t('What executor run when finish?'),
							isOptional: true,
							type: 'ExecutorPicker'
						}}
						onChange={(value) =>
							updateExecutor((a) => {
								if (a) {
									a.actionExecutorOnFinishId =
										value?.value === '' || value?.value === null || value === undefined
											? undefined
											: parseInt(value.value);
								}
							})
						}
						value={{
							isEncrypted: false,
							isValid: true,
							value:
								executor.actionExecutorOnFinishId === undefined
									? ''
									: executor.actionExecutorOnFinishId?.toString()
						}}
					/>
				</TwoColumnsGrid>
			</Column>
		</Row>
	);
};

export default ExecutorSettings;
