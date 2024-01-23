import { Input } from 'antd';
import { ActionScheduleDTO } from 'api/scheduleApi';
import { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

interface ScheduleNameEditorProps {
	schedule: ActionScheduleDTO;
	onChangeName: (newName: string) => void;
}

const ScheduleNameEditor = ({ schedule, onChangeName }: ScheduleNameEditorProps) => {
	const [tempName, setTempName] = useState(schedule.actionScheduleName);
	const [isEditing, setIsEditing] = useState(false);

	useEffect(() => {
		setTempName(schedule.actionScheduleName);
	}, [schedule.actionScheduleName]);
	const { t } = useTranslation();
	if (isEditing) {
		return (
			<Input
				autoFocus
				bordered={true}
				onBlur={() => {
					setIsEditing(false);
					onChangeName(tempName);
				}}
				onPressEnter={() => {
					setIsEditing(false);
					onChangeName(tempName);
				}}
				onChange={(e) => setTempName(e.target.value)}
				value={tempName}
			/>
		);
	} else {
		return (
			<div
				style={{ cursor: 'pointer' }}
				onClick={() => {
					setIsEditing((a) => !a);
				}}
			>
				{tempName ? tempName : t('Set name')}
			</div>
		);
	}
};

export default ScheduleNameEditor;
