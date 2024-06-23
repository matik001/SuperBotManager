import { useQuery, useQueryClient } from '@tanstack/react-query';
import { FloatButton, Table } from 'antd';
import { ColumnsType } from 'antd/es/table';
import { LogDTO, logGetAll, logKeys } from 'api/logApi';
import Spinner from 'components/UI/Spinners/Spinner';
import dayjs from 'dayjs';
import { useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import { MdOutlineRefresh } from 'react-icons/md';
import styled from 'styled-components';

const Container = styled.div``;

const LogViewer = () => {
	const logsQuery = useQuery({
		queryKey: logKeys.list(),
		queryFn: ({ signal }) => {
			return logGetAll(signal);
		}
	});
	const { t } = useTranslation();
	const columns = useMemo<ColumnsType<LogDTO>>(
		() => [
			{
				title: t('Date'),
				dataIndex: 'createdDate',
				render: (date) => dayjs(date).format('L LTS')
			},
			{
				title: t('Type'),
				dataIndex: 'logType' /// TODO dodac kolory
			},
			{
				title: t('Title'),
				dataIndex: 'logTitle'
			},
			{
				title: t('App'),
				dataIndex: 'logApp'
			},
			{
				title: t('Module'),
				dataIndex: 'logModule'
			},
			{
				title: t('User'),
				dataIndex: 'user',
				render: (_, r) => r.user?.userName
			}
		],

		[t]
	);
	const queryClient = useQueryClient();
	return (
		<>
			{logsQuery.isFetching || !logsQuery.data ? (
				<Spinner />
			) : (
				<>
					<Table
						// style={{ height: '100%', flexGrow: 1 }}
						rowKey={'id'}
						columns={columns}
						dataSource={logsQuery.data}
						expandable={{ expandedRowRender: (log) => <div>{log.logDetails}</div> }}
						// scroll={scroll}
					/>
					{!logsQuery.isFetching && (
						<FloatButton
							icon={<MdOutlineRefresh />}
							onClick={() => {
								queryClient.invalidateQueries({
									queryKey: logKeys.prefix
								});
								logsQuery.refetch();
							}}
						/>
					)}
				</>
			)}
		</>
	);
};

export default LogViewer;
