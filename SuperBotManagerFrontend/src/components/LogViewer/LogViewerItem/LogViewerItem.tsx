import { LogDTO } from 'api/logApi';
import dayjs from 'dayjs';

export interface LogViewerItemProps {
	log: LogDTO;
}
const LogViewerItem = ({ log }: LogViewerItemProps) => {
	return (
		<div>
			{log.logTitle} {log.logDetails} - {dayjs(log.createdDate).format('L LTS')}
		</div>
	);
};

export default LogViewerItem;
