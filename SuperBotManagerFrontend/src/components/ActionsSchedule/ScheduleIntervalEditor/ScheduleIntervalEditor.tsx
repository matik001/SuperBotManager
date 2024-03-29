import { Divider, InputNumber, Select, Space } from 'antd';
import { ActionScheduleDTO } from 'api/scheduleApi';
import IconButton from 'components/UI/IconButton/IconButton';
import dayjs from 'dayjs';
import { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { MdAdd } from 'react-icons/md';

interface ScheduleIntervalEditorProps {
	schedule: ActionScheduleDTO;
	onChangeInterval: (newInterval: number) => void;
}
const ScheduleIntervalEditor = ({ schedule, onChangeInterval }: ScheduleIntervalEditorProps) => {
	const { t } = useTranslation();
	const options = [
		{ value: 60 * 60 * 1000, label: t('Every hour') },
		{ value: 24 * 60 * 60 * 1000, label: t('Every day') },
		{ value: 7 * 24 * 60 * 60 * 1000, label: t('Every week') }
	];
	if (!options.some((x) => x.value === schedule.intervalSec)) {
		options.push({
			value: schedule.intervalSec,
			label: t('Every {{x}}', { x: dayjs.duration({ seconds: schedule.intervalSec }).humanize() })
		});
	}
	const [customInterval, setCustomInterval] = useState(schedule.intervalSec);
	return (
		<>
			<Select
				style={{ width: 315 }}
				onChange={(newInterval) => {
					onChangeInterval(newInterval);
				}}
				value={schedule.intervalSec}
				options={options}
				dropdownRender={(menu) => (
					<>
						{menu}
						<Divider style={{ margin: '8px 0' }} />
						<Space style={{ padding: '0 8px 4px' }}>
							<InputNumber
								style={{ width: '130px' }}
								suffix="sec"
								placeholder="Please enter time"
								value={customInterval}
								min={10}
								onChange={(val) => {
									setCustomInterval(val ?? 10);
								}}
								onPressEnter={() => onChangeInterval(customInterval)}
								// onKeyDown={(e) => e.stopPropagation()}
							/>
							<IconButton
								style={{ padding: '4px' }}
								type="text"
								onClick={(e) => {
									onChangeInterval(customInterval);
								}}
							>
								<MdAdd /> {t('Add custom interval')}
							</IconButton>
						</Space>
					</>
				)}
			/>
		</>
	);
};

export default ScheduleIntervalEditor;
