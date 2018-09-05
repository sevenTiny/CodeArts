package Arithmetic.Search;

import Arithmetic.CommonData;
import org.junit.Test;

public class BinaryTest {
    @Test
    public void test(){
        int result = Binary.Search(CommonData.SEQUENCE_ARRAY,3);
        System.out.println(result);
    }
}
