import { Button, Collapse, CollapseProps, Popconfirm } from 'antd';
import { FieldInfo } from 'api/actionDefinitionApi';
import { ExecutorInput } from 'api/actionExecutorApi';
import IconButton from 'components/UI/IconButton/IconButton';
import { useCallback, useMemo, useState } from 'react';
import { AiOutlinePlus } from 'react-icons/ai';
import { styled } from 'styled-components';
import { useCounter } from 'usehooks-ts';
import InputEditor from './InputEditor/InputEditor';

export interface InputsEditorProps {
	inputs: ExecutorInput[];
	inputSchema: FieldInfo[];
	onChangeInputs: (inputs: ExecutorInput[]) => void;
}
const Container = styled.div``;
const InputsEditor = ({ inputs, inputSchema, onChangeInputs }: InputsEditorProps) => {
	const { count: nextInputId, increment: incNextInputId } = useCounter(0);
	const [inputsIds, setInputIds] = useState(() => inputs.map((_) => Math.random() * 1000000));

	const onDeleteInput = useCallback(
		(idx: number) => {
			const id = inputsIds[idx];
			setInputIds(inputsIds.filter((a) => a !== id));
			onChangeInputs(inputs.filter((_, idxx) => idxx !== idx));
		},
		[inputs, inputsIds, onChangeInputs] /// in order to useCallback had any sense, we should remove inputs from dependencies
	);
	const onAddInput = () => {
		setInputIds([...inputsIds, nextInputId]);
		onChangeInputs([...inputs, {}]);
		incNextInputId();
	};
	const items: CollapseProps['items'] = useMemo(
		() =>
			inputs.map((input, idx) => ({
				key: inputsIds[idx],
				label: `Input ${idx + 1}`,
				extra: (
					<Popconfirm
						title="Delete input"
						description="Are you sure to delete this input?"
						onConfirm={() => onDeleteInput(idx)}
						okText="Yes"
						cancelText="No"
						onPopupClick={(e) => e.stopPropagation()}
					>
						<Button danger type="text" onClick={(e) => e.stopPropagation()}>
							Delete
						</Button>
					</Popconfirm>
				),
				children: (
					<InputEditor
						key={idx}
						input={input}
						inputSchema={inputSchema}
						onChangeInput={(input) => {
							const newInputs = [
								...inputs.slice(0, idx),
								input,
								...inputs.slice(idx + 1, inputs.length)
							];
							onChangeInputs(newInputs);
							return newInputs;
						}}
					/>
				)
			})) as CollapseProps['items'],
		[inputSchema, inputs, inputsIds, onChangeInputs, onDeleteInput]
	);

	return (
		<div>
			<IconButton type="primary" onClick={onAddInput}>
				<AiOutlinePlus />
				Add input
			</IconButton>
			<Collapse bordered={true} accordion items={items} />
		</div>
	);
};

export default InputsEditor;
