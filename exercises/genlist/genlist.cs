

public class genlist<T>{
	public T[] data;
	public int size => data.Length; // property
	public T this[int i] => data[i]; // indexer
	public genlist(){ data = new T[0]; }
	public void add(T item){ 
        /* add item to the list */
		T[] newdata = new T[size+1];
		System.Array.Copy(data,newdata,size);
		newdata[size]=item;
		data=newdata;
	}
    public void remove(int i){
        /* removes element i */
        T[] newdata = new T[size-1];
        for(int index =0; index<i; index++){
            newdata[index] = data[index];
        }
        for(int index=i; index < size-1; index++){
            newdata[index] = data[index+1];
        }
        data = newdata;
    }
}
