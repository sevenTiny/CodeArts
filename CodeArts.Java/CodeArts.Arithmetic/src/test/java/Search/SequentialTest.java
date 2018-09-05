package Search;

import Common.CommonData;
import org.junit.Test;

public class SequentialTest {
    @Test
    public void test(){
        int result = Sequential.Search(CommonData.SEQUENCE_ARRAY,3);
        System.out.println(result);
    }
}
