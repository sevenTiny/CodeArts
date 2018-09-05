package Arithmetic.Sort;

import Arithmetic.CommonData;
import org.junit.Test;

public class SelectTest {
    @Test
    public void test() {
        Select.Sort(CommonData.OUT_OF_ORDER_ARRAY);

        for (int a : CommonData.OUT_OF_ORDER_ARRAY) {
            System.out.println(a);
        }
    }
}
