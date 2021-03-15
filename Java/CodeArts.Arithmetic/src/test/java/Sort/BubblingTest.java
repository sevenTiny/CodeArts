package Sort;

import Common.CommonData;
import org.junit.Test;

public class BubblingTest {
    @Test
    public void test() {
        Bubbling.Sort(CommonData.OUT_OF_ORDER_ARRAY);

        for (int a : CommonData.OUT_OF_ORDER_ARRAY) {
            System.out.println(a);
        }
    }
}
