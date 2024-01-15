import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { DatePicker, Popconfirm, Select, Space, Switch, Table, TableProps } from 'antd';
import {
	ActionScheduleCreateDTO,
	ActionScheduleDTO,
	ActionScheduleUpdateDTO,
	scheduleCreate,
	scheduleDelete,
	scheduleGetAll,
	scheduleKeys,
	schedulePut
} from 'api/scheduleApi';
import FieldExecutorPickerEditor from 'components/ActionExecutor/ActionExecutorEditor/InputsEditor/InputEditor/FieldEditor/FieldExecutorPickerEditor/FieldExecutorPickerEditor';
import IconButton from 'components/UI/IconButton/IconButton';
import { ScrollableMixin } from 'components/UI/Scrollable/Scrollable';
import Spinner from 'components/UI/Spinners/Spinner';
import dayjs from 'dayjs';
import React from 'react';
import { IoMdTrash } from 'react-icons/io';
import { MdAdd } from 'react-icons/md';
import { styled } from 'styled-components';
import ScheduleIntervalEditor from './ScheduleIntervalEditor/ScheduleIntervalEditor';
import ScheduleNameEditor from './ScheduleNameEditor/ScheduleNameEditor';

interface ActionsScheduleProps {}

const Container = styled.div`
	background-color: ${(p) => p.theme.bgColor};
	margin: 0 10px;
	padding: 30px;
	${ScrollableMixin}
`;

const ActionsSchedule: React.FC<ActionsScheduleProps> = ({}) => {
	const { data, isFetching } = useQuery({
		queryKey: scheduleKeys.list(),
		queryFn: ({ signal }) => scheduleGetAll(signal),
		refetchInterval: 10000
	});

	const queryClient = useQueryClient();
	const changeScheduleMutation = useMutation({
		mutationFn: (schedule: ActionScheduleUpdateDTO) => schedulePut(schedule),
		onSuccess: () => queryClient.invalidateQueries({ queryKey: scheduleKeys.prefix })
	});
	const addScheduleMutation = useMutation({
		mutationFn: (schedule: ActionScheduleCreateDTO) => scheduleCreate(schedule),
		onSuccess: () => queryClient.invalidateQueries({ queryKey: scheduleKeys.prefix })
	});
	const deleteScheduleMutation = useMutation({
		mutationFn: (id: number) => scheduleDelete(id),
		onSuccess: () => queryClient.invalidateQueries({ queryKey: scheduleKeys.prefix })
	});
	const columns: TableProps<ActionScheduleDTO>['columns'] = [
		{
			title: 'Enabled',
			dataIndex: 'enabled',
			key: 'enabled',
			render: (_, obj) => {
				return (
					<Switch
						checked={obj.enabled}
						onChange={(newVal) => changeScheduleMutation.mutate({ ...obj, enabled: newVal })}
					/>
				);
			}
		},
		{
			title: 'Name',
			dataIndex: 'actionScheduleName',
			key: 'actionScheduleName',
			render: (_, obj) => (
				<ScheduleNameEditor
					schedule={obj}
					onChangeName={(newName) =>
						changeScheduleMutation.mutate({ ...obj, actionScheduleName: newName })
					}
				/>
			)
		},
		{
			title: 'Executor',
			dataIndex: 'executorId',
			key: 'executorId',
			render: (_, obj) => (
				<FieldExecutorPickerEditor
					fieldWidthPx={200}
					fieldSchema={{
						description: 'Pick an executor',
						isOptional: false,
						name: '',
						type: 'ExecutorPicker'
					}}
					onChange={(executorValue) =>
						changeScheduleMutation.mutate({ ...obj, executorId: parseInt(executorValue.value!) })
					}
					value={{
						isEncrypted: false,
						isValid: true,
						value: obj.executorId.toString()
					}}
				/>
			)
		},
		{
			title: 'Next run',
			dataIndex: 'nextRun',
			key: 'nextRun',
			render: (_, schedule) => {
				return (
					<DatePicker
						showTime
						value={dayjs(schedule.nextRun)}
						allowClear={false}
						onChange={(e) => changeScheduleMutation.mutate({ ...schedule, nextRun: e!.toDate() })}
					/>
				);
			}
		},
		{
			title: 'Type',
			key: 'type',
			dataIndex: 'type',
			render: (_, schedule) => (
				<>
					<Select
						style={{ width: 120 }}
						value={schedule.type}
						options={[
							{ value: 'Once', label: 'Once' },
							{ value: 'Period', label: 'Period' }
						]}
						onChange={(newType) => {
							changeScheduleMutation.mutate({ ...schedule, type: newType });
						}}
					/>
				</>
			)
		},
		{
			title: 'Interval',
			key: 'intervalSec',
			dataIndex: 'intervalSec',
			render: (_, schedule) => {
				if (schedule.type === 'Once') return <></>;
				return (
					<ScheduleIntervalEditor
						onChangeInterval={(newInterval) =>
							changeScheduleMutation.mutate({ ...schedule, intervalSec: newInterval })
						}
						schedule={schedule}
					/>
				);
			}
		},
		{
			title: 'Action',
			key: 'action',
			render: (_, record) => (
				<Space size="middle">
					<Popconfirm
						title="Delete input"
						description="Are you sure to delete this schedule?"
						onConfirm={() => deleteScheduleMutation.mutate(record.id)}
						okText="Yes"
						cancelText="No"
					>
						<IconButton danger type="default">
							<IoMdTrash />
							Delete
						</IconButton>
					</Popconfirm>
				</Space>
			)
		}
	];

	return (
		<Container>
			{(isFetching ||
				addScheduleMutation.isPending ||
				deleteScheduleMutation.isPending ||
				changeScheduleMutation.isPending) && <Spinner />}
			<Space align="center" style={{ marginBottom: '10px' }}>
				<h1 style={{ fontWeight: 300, fontSize: '32px' }}>Schedule</h1>
				<IconButton
					shape="circle"
					style={{ marginLeft: '0px', fontSize: '26px' }}
					type="text"
					onClick={() =>
						addScheduleMutation.mutate({
							enabled: false,
							actionScheduleName: 'New schedule',
							executorId: 0,
							intervalSec: 120,
							nextRun: new Date(new Date().getTime() + 60000),
							type: 'Period'
						})
					}
				>
					<MdAdd />
				</IconButton>
			</Space>
			<Table columns={columns} dataSource={data} pagination={{ pageSize: 5 }} />
		</Container>
	);
};

export default ActionsSchedule;
