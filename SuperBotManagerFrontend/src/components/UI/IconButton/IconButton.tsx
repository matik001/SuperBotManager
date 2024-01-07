import { Button } from 'antd';
import type { ComponentProps } from 'react';

const IconButton = ({ style, ...props }: ComponentProps<typeof Button>) => {
	return (
		<Button
			style={{
				...style,
				display: 'flex',
				flexDirection: style?.flexDirection ?? 'row',
				alignItems: style?.alignItems ?? 'center',
				justifyContent: 'center',
				gap: style?.gap ?? '5px'
			}}
			{...props}
		></Button>
	);
};

export default IconButton;
